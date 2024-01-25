using Microsoft.Extensions.Configuration;
using movie.application.DTO.Response;
using movie.application.MovieApiIntegration.Interface;
using Newtonsoft.Json;

namespace movie.application.MovieApiIntegration.Implementation
{
    public class MovieClient : IMovieClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public MovieClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("MovieApiConfig:BaseUrl");
            _apiKey = _configuration.GetValue<string>("MovieApiConfig:ApiKey");
        }

        public async Task<(SearchMovieResponseDTO, int)> GetMovies(string title, int pageNumber)
        {
            var omdbApiUrl = $"{_baseUrl}?apikey={_apiKey}&s={title}&page={pageNumber}";
            var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.GetAsync(omdbApiUrl);
            var httpStringResponse = await httpResponse.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<SearchMovieResponseDTO>(httpStringResponse), (int)httpResponse.StatusCode);
        }

        public async Task<(MovieDetails, int)> GetMovieDetails(string movieId)
        {     
            var omdbApiUrl = $"{_baseUrl}?i={movieId}&apikey={_apiKey}";
            var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.GetAsync(omdbApiUrl);
            var httpStringResponse = await httpResponse.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<MovieDetails>(httpStringResponse), (int)httpResponse.StatusCode);
        }  
    }
}
