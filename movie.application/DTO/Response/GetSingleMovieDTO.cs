namespace movie.application.DTO.Response
{
    public class GetSingleMovieDTO
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Duration { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }

        public string imdbRating { get; set; }
        public string imdbID { get; set; }
    }
}
