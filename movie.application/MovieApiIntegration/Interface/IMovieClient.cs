using movie.application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie.application.MovieApiIntegration.Interface
{
    public interface IMovieClient
    {
        Task<(SearchMovieResponseDTO, int)> GetMovies(string title);
        Task<(MovieDetails, int)> GetMovieDetails(string movieId);
    }
}
