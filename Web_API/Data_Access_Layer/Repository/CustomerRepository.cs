
using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMovies()
        {
            return await _context.Movies
                .Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesByDate(DateTime date)
        {
            return await _context.Movies
                .Where(m => m.Shows.Any(s => s.StartDate <= date && s.EndDate >= date))
                .Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description
                })
                .ToListAsync();
        }

        public async Task<int> GetAvailableSeats(int showId)
        {
            var show = await _context.Shows
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.ShowId == showId);

            var bookedSeats = show.Tickets.Sum(t => t.NumberOfSeats);
            var availableSeats = show.NumberOfSeats - bookedSeats;

            return availableSeats;
        }

        public async Task<TicketDTO> BookTicket(TicketDTO ticketDTO)
         {
             var show = await _context.Shows
                 .Include(s => s.Tickets)
                 .Include(s => s.Movie)
                 .FirstOrDefaultAsync(s => s.ShowId == ticketDTO.ShowId);

             if (ticketDTO.NumberOfSeats > show?.NumberOfSeats)
             {
                 throw new Exception("Not enough seats available.");
             }

             var ticket = new Ticket
             {
                 TicketId = ticketDTO.TicketId,
                 UserId = ticketDTO.UserId,
                 ShowId = ticketDTO.ShowId,
                 NumberOfSeats = ticketDTO.NumberOfSeats,
                 BookingDate = DateTime.Now
             };

             _context.Tickets.Add(ticket);
             show!.NumberOfSeats -= ticketDTO.NumberOfSeats;
             await _context.SaveChangesAsync();

             return new TicketDTO
             {
                 TicketId = ticket.TicketId,
                 MovieName = show.Movie!.Name,
                 ShowTime = show.Timing,
                 ScreenNumber = show.ScreenNumber,
                 BookingDate = ticket.BookingDate,
                 NumberOfSeats = ticket.NumberOfSeats
             };
         }
 
        public async Task<IEnumerable<TicketDTO>> GetBookedTickets(string userId)
        {
            return await _context.Tickets
                .Include(t => t.Show)
                .ThenInclude(s => s.Movie)
                .Where(t => t.UserId == userId)
                .Select(t => new TicketDTO
                {
                    TicketId = t.TicketId,
                    MovieName = t.Show.Movie.Name,
                    ShowTime = t.Show.Timing,
                    UserId=t.UserId,
                    BookingDate = t.BookingDate,
                    NumberOfSeats = t.NumberOfSeats,
                    ScreenNumber = t.Show.ScreenNumber,
                    ShowId = t.ShowId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ShowDTO>> GetAllShows()
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Select(s => new ShowDTO
                {
                    ShowId = s.ShowId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Timing = s.Timing,
                    NumberOfSeats = s.NumberOfSeats,
                    Price = s.Price,
                    ScreenNumber = s.ScreenNumber,
                    MovieId = s.Movie.MovieId,
                    MovieName = s.Movie.Name
                })
                .ToListAsync();
        }
    }
}
