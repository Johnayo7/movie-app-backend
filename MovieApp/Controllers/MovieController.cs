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

        public MovieController(ISearchService searchService)
        {
            _searchService = searchService;
        }



        //private const string ApiKey = "f467275d";


        [HttpGet("Search/{title}")]
        public async Task<IActionResult> SearchMovies(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest(new ApiResponseDTO<string> { Status = false, Message = "Title cannot be Empty"});

            try
            {
                var (responseResult, statusCode) = await _searchService.SearchMovies(title);
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


 
