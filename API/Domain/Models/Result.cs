using System.Net;

namespace API.Domain.Models
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public HttpStatusCode StatusCode { get; set; }
        public object? Data { get; private set; }

        private Result(bool isSuccess, HttpStatusCode statusCode, string message = null, object data = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        // Cria um Result de sucesso
        public static Result Success(HttpStatusCode statusCode, object data = null, string? message = null)
        {
            return new Result(true, statusCode, message, data);
        }

        // Cria um Result de falha
        public static Result Failure(HttpStatusCode statusCode, string errorMessage)
        {
            return new Result(false, statusCode, errorMessage);
        }
    }
}
