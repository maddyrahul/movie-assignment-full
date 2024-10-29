using Data_Access_Layer.DTOs;

namespace Business_Layer.Services
{
    public interface IAdminService
    {
        Task<List<ShowDTO>> GetShowsByDateAsync(DateTime date);
        Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO);
        Task<bool> AddShowToMovieAsync(int movieId, ShowDTO showDTO);
        Task<MovieDTO> GetMovieByIdAsync(int id);
    }
}
