using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;

namespace Business_Layer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<ShowDTO>> GetShowsByDateAsync(DateTime date)
        {
            var shows = await _adminRepository.GetShowsByDateAsync(date);
            var showDTOs = new List<ShowDTO>();

            foreach (var show in shows)
            {
                showDTOs.Add(new ShowDTO
                {
                    ShowId = show.ShowId,
                    StartDate = show.StartDate,
                    EndDate = show.EndDate,
                    Timing = show.Timing,
                    NumberOfSeats = show.NumberOfSeats,
                    Price = show.Price,
                    ScreenNumber = show.ScreenNumber,
                    MovieId = show.Movie.MovieId,
                    MovieName = show.Movie.Name
                });
            }
            return showDTOs;
        }

        public async Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO)
        {
            if (string.IsNullOrEmpty(movieDTO.Name) || string.IsNullOrEmpty(movieDTO.Genre) || string.IsNullOrEmpty(movieDTO.Director))
            {
                throw new ArgumentException("Movie details are incomplete.");
            }

            var movie = new Movie
            {
                Name = movieDTO.Name,
                Genre = movieDTO.Genre,
                Director = movieDTO.Director,
                Description = movieDTO.Description
            };

            await _adminRepository.AddMovieAsync(movie);
            movieDTO.MovieId = movie.MovieId;
            return movieDTO;
        }

        public async Task<bool> AddShowToMovieAsync(int movieId, ShowDTO showDTO)
        {
            // Validate screen number
            if (showDTO.ScreenNumber < 1 || showDTO.ScreenNumber > 3)
            {
                throw new ArgumentException("Screen number must be between 1 and 3, as the theater has a minimum of 3 screens.");
            }

            // Check for overlapping showtimes
            if (await _adminRepository.ShowExistsOnScreenAsync(showDTO.ScreenNumber, showDTO.StartDate, showDTO.EndDate))
            {
                throw new ArgumentException($"A show already exists at screen {showDTO.ScreenNumber} with the same time or overlapping dates.");
            }

            var show = new Show
            {
                MovieId = movieId,
                StartDate = showDTO.StartDate,
                EndDate = showDTO.EndDate,
                Timing = showDTO.Timing,
                NumberOfSeats = showDTO.NumberOfSeats,
                Price = showDTO.Price,
                ScreenNumber = showDTO.ScreenNumber
            };

            await _adminRepository.AddShowAsync(show);
            return true;
        }

        public async Task<MovieDTO> GetMovieByIdAsync(int id)
        {
            var movie = await _adminRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return null;
            }

            return new MovieDTO
            {
                MovieId = movie.MovieId,
                Name = movie.Name,
                Genre = movie.Genre,
                Director = movie.Director,
                Description = movie.Description
            };
        }
    }
}
