
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Show>> GetShowsByDateAsync(DateTime date)
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Where(s => s.StartDate <= date && s.EndDate >= date)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Shows).FirstOrDefaultAsync(m => m.MovieId == id);
        }

        
        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ShowExistsOnScreenAsync(int screenNumber, DateTime startDate, DateTime endDate)
        {
            return await _context.Shows.AnyAsync(s =>
                s.StartDate <= endDate && s.EndDate >= startDate);
        }

        public async Task AddShowAsync(Show show)
        {
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
        }
    }
}
