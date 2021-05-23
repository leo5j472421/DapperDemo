namespace DapperTest.Models
{
    public class ApiResponse<T>
    {
        public T Data { get;  }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
            ErrorCode = 0;
            ErrorMessage = "No Error";
        }
    }
}