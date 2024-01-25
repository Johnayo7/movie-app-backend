using Microsoft.Extensions.Caching.Memory;
using movie.application.DTO.Response;
using movie.application.MovieApiIntegration.Interface;
using movie.application.Services.Interface;
using movie.domain.Models;

namespace movie.application.Services.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly IMovieClient _movieClient;
        private readonly IMemoryCache _cache;

        public SearchService(IMovieClient movieClient, IMemoryCache cache)
        {
            _movieClient = movieClient;
            _cache = cache;
        }

        public async Task<(ApiResponseDTO<List<GetMoviesResponseDTO>>, int)> SearchMovies(string movieTitle)
        {
            var (movies, statusCode) = await _movieClient.GetMovies(movieTitle);

            if (statusCode != 200)
            {
                return (new ApiResponseDTO<List<GetMoviesResponseDTO>> { Status = false, Message = "Error occured while fetching movies" }, 500);
            }

            var moviesToReturn = new List<GetMoviesResponseDTO>();

            foreach (var movie in movies.Search)
            {
                moviesToReturn.Add(new GetMoviesResponseDTO()
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    imdbID = movie.imdbID,
                    Type = movie.Type,
                    Poster = movie.Poster
                });
            }

            SaveSearchQuery(movieTitle);
            return (new ApiResponseDTO<List<GetMoviesResponseDTO>> { Status = true, Message = "Successfully retrieve movies", Data = moviesToReturn }, 200);

        }

        public async Task<(ApiResponseDTO<GetSingleMovieDTO>, int)> GetSingleMovie(string imDbId)
        {
            var (movie, statusCode) = await _movieClient.GetMovieDetails(imDbId);

            if (statusCode != 200)
            {
                return (new ApiResponseDTO<GetSingleMovieDTO> { Status = false, Message = "Error occured while fetching movies" }, 500);
            }

            if (movie.Response == "False")
            {
                return (new ApiResponseDTO<GetSingleMovieDTO> { Status = false, Message = movie.Error }, 500);

            }

            var movieToReturn = new GetSingleMovieDTO
            {
                Title = movie.Title,
                Year = movie.Year,
                Rated = movie.Rated,
                Released = movie.Released,
                Duration = movie.Runtime,
                Genre = movie.Genre,
                Description = movie.Plot,
                Poster = movie.Poster,
                imdbRating = movie.imdbRating,
                imdbID = movie.imdbID
            };



            return (new ApiResponseDTO<GetSingleMovieDTO> { Status = true, Message = "Successfully retrieve movies", Data = movieToReturn }, 200);

        }



        public void SaveSearchQuery(string saveQueries)
        {
            var queriesFromCache = _cache.Get<List<string>>("movies");

            if (queriesFromCache == null || !queriesFromCache.Any())
            {
                _cache.Set<List<string>>("movies", new List<string> { saveQueries });
            }

            else if (queriesFromCache.Count > 0 && queriesFromCache.Count < 5)
            {
                queriesFromCache.Add(saveQueries);
                _cache.Set<List<string>>("movies", queriesFromCache);

            }

            else
            {
                queriesFromCache.RemoveAt(0);
                queriesFromCache.Add(saveQueries);
                _cache.Set<List<string>>("movies", queriesFromCache);

            }
        }

        public async Task<(ApiResponseDTO<List<string>>, int)> GetLatestSearchQueriesAsync()
        {
            var queriesFromCache = _cache.Get<List<string>>("movies");


            return (new ApiResponseDTO<List<string>> { Status = true, Message = "Successfully retrieve movies", Data = queriesFromCache }, 200);

        }


    }
}
