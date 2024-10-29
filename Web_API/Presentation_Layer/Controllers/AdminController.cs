using Business_Layer.Services;
using Data_Access_Layer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // 1. View all current shows for a specific date
        [HttpGet("shows/{date}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<ShowDTO>>> GetShowsByDate(DateTime date)
        {
            try
            {
                var shows = await _adminService.GetShowsByDateAsync(date);
                return Ok(shows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 2. Add a new movie with multiple showtimes
        [HttpPost("movies")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MovieDTO>> AddMovie(MovieDTO movieDTO)
        {
            try
            {
                var addedMovie = await _adminService.AddMovieAsync(movieDTO);
                return CreatedAtAction(nameof(GetMovieById), new { id = addedMovie.MovieId }, addedMovie);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 3. Add additional showtime to an existing movie
        [HttpPost("movies/{movieId}/show")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ShowDTO>> AddShowToMovie(int movieId, ShowDTO showDTO)
        {
            try
            {
                var movie = await _adminService.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return NotFound("Movie not found.");
                }

                var success = await _adminService.AddShowToMovieAsync(movieId, showDTO);
                if (success)
                {
                    return Ok(showDTO);
                }
                return BadRequest("Failed to add show.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Helper method to get movie by id
        [HttpGet("movies/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
        {
            try
            {
                var movie = await _adminService.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound("Movie not found.");
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
