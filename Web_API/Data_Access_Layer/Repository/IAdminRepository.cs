using Data_Access_Layer.Models;


namespace Data_Access_Layer.Repositories
{

    public interface IAdminRepository
    {
        Task<IEnumerable<Show>> GetShowsByDateAsync(DateTime date);
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task<bool> ShowExistsOnScreenAsync(int screenNumber, DateTime startDate, DateTime endDate);
        Task AddShowAsync(Show show);
    }
}
