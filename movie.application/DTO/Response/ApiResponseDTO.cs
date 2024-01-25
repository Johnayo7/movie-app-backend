namespace movie.application.DTO.Response
{
    public class ApiResponseDTO<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
