
namespace GameSeat.Backend.Infrastructure.Data.DTOs
{
    public class ServiceResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } 

        public ServiceResultDTO(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ServiceResultDTO Successful(string message = "Operation successful", object data = null)
        {
            return new ServiceResultDTO(true, message, data);
        }

        public static ServiceResultDTO Failure(string message = "Operation failed", object data = null)
        {
            return new ServiceResultDTO(false, message, data);
        }
    }
}
