using Data_Access_Layer.DTOs;

namespace Data_Access_Layer.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<MovieDTO>> GetAllMovies();
        Task<IEnumerable<MovieDTO>> GetMoviesByDate(DateTime date);
        Task<int> GetAvailableSeats(int showId);
        Task<TicketDTO> BookTicket(TicketDTO ticketDTO);
        Task<IEnumerable<TicketDTO>> GetBookedTickets(string userId);
        Task<IEnumerable<ShowDTO>> GetAllShows();
    }
}
