using Business_Layer.Services;
using Data_Access_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("movies")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAllMovies()
        {
            var movies = await _customerService.GetAllMovies();
            if (!movies.Any())
            {
                return NotFound("No movies found.");
            }
            return Ok(movies);
        }

        [HttpGet("movies/{date}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesByDate(DateTime date)
        {
            var movies = await _customerService.GetMoviesByDate(date);
            return Ok(movies);
        }

        [HttpGet("shows/{showId}/seats")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<int>> GetAvailableSeats(int showId)
        {
            var availableSeats = await _customerService.GetAvailableSeats(showId);

            if (availableSeats < 0 || availableSeats==0)
            {
                return BadRequest("No seats are Available .");
            }

            return Ok(availableSeats);
        }


        [HttpPost("tickets")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<TicketDTO>> BookTicket(TicketDTO ticketDTO)
        {
            try
            {
                var confirmation = await _customerService.BookTicket(ticketDTO);
                return Ok(confirmation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tickets/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetBookedTickets(string userId)
        {
            var tickets = await _customerService.GetBookedTickets(userId);
            if (!tickets.Any())
            {
                return NotFound("No tickets found for this user.");
            }
            return Ok(tickets);
        }

        [HttpGet("shows")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<ShowDTO>>> GetAllShows()
        {
            var shows = await _customerService.GetAllShows();
            if (!shows.Any())
            {
                return NotFound("No shows found.");
            }
            return Ok(shows);
        }
    }
}
