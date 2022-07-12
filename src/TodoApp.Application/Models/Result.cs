using System.Net;
using System.Text.Json.Serialization;
using TodoApp.Domain.Models;

namespace TodoApp.Application.Models
{
    public class Result<T>
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; protected set; }
        public int? TotalRows { get; protected set; }
        public T Data { get; protected set; }
        public IReadOnlyCollection<Notification> Errors => _errors.AsReadOnly();

        protected readonly List<Notification> _errors;

        protected Result()
        {
            _errors = new List<Notification>();
        }

        public Result(T data, int? total = null) : this()
        {
            Data = data;
            TotalRows = total;
        }

        public Result(HttpStatusCode statusCode, string code, string message) : this()
        {
            _errors.Add(new Notification(statusCode, code, message));
            StatusCode = statusCode;
        }

        public Result(HttpStatusCode statusCode, IEnumerable<Notification> errors) : this()
        {
            if (errors != null) _errors.AddRange(errors);
            StatusCode = statusCode;
        }

        public void AddError(Notification error) => _errors.Add(error);
        public void AddErrors(IEnumerable<Notification> errors) => _errors.AddRange(errors);
    }

    public class Result : Result<dynamic>
    {
        public Result() : base() { }

        public Result(dynamic data) : base() => Data = data;

        public Result(HttpStatusCode statusCode, string code, string message) : base(statusCode, code, message) { }

        public Result(HttpStatusCode statusCode, IEnumerable<Notification> errors) : base(statusCode, errors) { }
    }
}
