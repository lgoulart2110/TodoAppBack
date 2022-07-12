using System.Net;
using System.Text.Json.Serialization;

namespace TodoApp.Domain.Models
{
    public class Notification
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; private set; }
        public string Code { get; private set; }
        public string Message { get; private set; }

        public Notification(HttpStatusCode statusCode, string code, string message)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
        }
    }
}
