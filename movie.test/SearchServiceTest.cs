using Microsoft.Extensions.Caching.Memory;
using Moq;
using movie.application.DTO.Response;
using movie.application.MovieApiIntegration.Interface;
using movie.application.Services.Implementation;
using movie.application.Services.Interface;

namespace movie.test
{
    public class SearchServiceTest
    {
        private readonly Mock<IMovieClient> _mockMovieClient;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ICacheHandler> _mockCacheHandler;
        private readonly SearchService _sut;

        public SearchServiceTest()
        {
            _mockMovieClient = new Mock<IMovieClient>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockCacheHandler = new Mock<ICacheHandler>();
            _sut = new SearchService(_mockMovieClient.Object, _mockMemoryCache.Object, _mockCacheHandler.Object);
        }

        [Fact]
        public async Task SearchMovies_DoesNotReturnNull()
        {
            // Arrange
            var getMovieReturnObject = new SearchMovieResponseDTO
            {
                Search = new List<Search> { new Search { Title = "star wars", imdbID = "tt002345",
                Poster = "www.thumbnail.com", Type = "movie", Year = "2009" } }
            };

            _mockMovieClient.Setup(s => s.GetMovies("star wars", 1)).ReturnsAsync((getMovieReturnObject, 200));

            _mockCacheHandler.Setup(s => s.SaveSearchQuery("star wars"));

            // Act
            var result = await _sut.SearchMovies("star wars", 1);

            // Assert
            Assert.NotNull(result);
         
        }

        [Fact]
        public async Task SearchMovies_ReturnsListOfMovies()
        {
            // Arrange
            var getMovieReturnObject = new SearchMovieResponseDTO
            {
                Search = new List<Search> { new Search { Title = "star wars", imdbID = "tt002345",
                Poster = "www.thumbnail.com", Type = "movie", Year = "2009" } }
            };

            _mockMovieClient.Setup(s => s.GetMovies("star wars", 1)).ReturnsAsync((getMovieReturnObject, 200));

            _mockCacheHandler.Setup(s => s.SaveSearchQuery("star wars"));

            // Act
            var result = await _sut.SearchMovies("star wars", 1);

            // Assert
            Assert.IsType<ApiResponseDTO<List<GetMoviesResponseDTO>>>(result.Item1);

        }
    }
}