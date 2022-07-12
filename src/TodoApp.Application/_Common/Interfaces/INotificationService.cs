using System.Net;
using TodoApp.Domain.Models;

namespace TodoApp.Application._Common.Interfaces 
{ 
    public interface INotificationService
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
        void AddNotification(HttpStatusCode statusCode, string key, string message);
        void AddNotification(Notification notification);
        void AddNotifications(IEnumerable<Notification> notifications);
    }
}
