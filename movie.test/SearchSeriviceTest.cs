using Microsoft.Extensions.Caching.Memory;
using Moq;
using movie.application.DTO.Response;
using movie.application.MovieApiIntegration.Interface;
using movie.application.Services.Implementation;

namespace movie.test
{
    public class SearchSeriviceTest
    {
        private readonly Mock<IMovieClient> _mockMovieClient;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly SearchService _sut;

        public SearchServiceTest()
        {
            _mockMovieClient = new Mock<IMovieClient>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _sut = new SearchService(_mockMovieClient.Object, _mockMemoryCache.Object);
        }

        [Fact]
        public void SearchMovies_ReturnsListOfMovies()
        {
            _mockMovieClient.Setup(s => s.GetMovies("", 1)).ReturnsAsync((new SearchMovieResponseDTO { }, 200));
        }
    }
}