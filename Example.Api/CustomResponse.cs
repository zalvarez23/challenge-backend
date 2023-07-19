namespace Example.Api
{
    public class CustomResponse<T>
    {
        public string? Status { get; set; }
        public T Data { get; set; }

        public CustomResponse(string status, T data)
        {
            Status = status;
            Data = data;
        }

    }
}
