using movie.application.DTO.Response;
using movie.domain.Models;

namespace movie.application.Services.Interface
{
    public interface ISearchService
    {
        Task<(ApiResponseDTO<List<GetMoviesResponseDTO>>, int)> SearchMovies(string movieTitle);
        Task<(ApiResponseDTO<GetSingleMovieDTO>, int)> GetSingleMovie(string imDbId);
        Task<(ApiResponseDTO<List<string>>, int)> GetLatestSearchQueriesAsync();

    }
}
