namespace MyWebApp.Models
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }   
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
