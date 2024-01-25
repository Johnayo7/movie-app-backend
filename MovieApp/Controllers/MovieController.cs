using Microsoft.AspNetCore.Mvc;
using movie.application.DTO.Response;
using movie.application.Services.Interface;
using movie.domain.Models;
using Newtonsoft.Json.Linq;

namespace movie.api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IConfiguration _configuration;

        public MovieController(ISearchService searchService, IConfiguration configuration)
        {
            _searchService = searchService;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> LandingPage (int pageNumber = 1)
        {
            try
            {
                var landingPageKeyWord = _configuration.GetValue<string>("LandingPage");
                var (responseResult, statusCode) = await _searchService.SearchMovies(landingPageKeyWord, pageNumber);
                return StatusCode(statusCode, responseResult);
            }
            catch (Exception)
            {
                // Handle API request error
                return StatusCode(500, "Error searching movies from OMDB API");
            }
        }

        [HttpGet("Search/{title}")]
        public async Task<IActionResult> SearchMovies(string title, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest(new ApiResponseDTO<string> { Status = false, Message = "Title cannot be Empty"});

            try
            {
                var (responseResult, statusCode) = await _searchService.SearchMovies(title, pageNumber);
                return StatusCode(statusCode, responseResult);  
            }
            catch (Exception)
            {
                // Handle API request error
                return StatusCode(500, "Error searching movies from OMDB API");
            }
        }

        [HttpGet("{imDbId}")]
        public async Task <IActionResult> GetMovieDetails(string imDbId)
        {
            if (string.IsNullOrEmpty(imDbId))
                return BadRequest(new ApiResponseDTO<string> { Status = false, Message = "Title cannot be Empty" });

            {
                try
                {
                    var (responseResult, statusCode) = await _searchService.GetSingleMovie(imDbId);
                    return StatusCode(statusCode, responseResult);
                }
                catch (Exception)
                {
                    // Handle API request error
                    return StatusCode(500, "Error retrieving movie details from OMDB API");
                }
            }
        }

        [HttpGet("latestSearches")]
        public async Task<IActionResult> GetLatestSearchQueries()
        {
            var (responseResult, statusCode) = await _searchService.GetLatestSearchQueriesAsync();
            return StatusCode(statusCode, responseResult);
        }
    }
}


 
