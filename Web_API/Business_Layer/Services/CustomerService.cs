using Data_Access_Layer.DTOs;
using Data_Access_Layer.Repositories;

namespace Business_Layer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMovies()
        {
            return await _customerRepository.GetAllMovies();
        }

        public async Task<IEnumerable<MovieDTO>> GetMoviesByDate(DateTime date)
        {
            return await _customerRepository.GetMoviesByDate(date);
        }

        public async Task<int> GetAvailableSeats(int showId)
        {
            return await _customerRepository.GetAvailableSeats(showId);
        }

        public async Task<TicketDTO> BookTicket(TicketDTO ticketDTO)
        {
            return await _customerRepository.BookTicket(ticketDTO);
        }

        public async Task<IEnumerable<TicketDTO>> GetBookedTickets(string userId)
        {
            return await _customerRepository.GetBookedTickets(userId);
        }

        public async Task<IEnumerable<ShowDTO>> GetAllShows()
        {
            return await _customerRepository.GetAllShows();
        }
    }
}
