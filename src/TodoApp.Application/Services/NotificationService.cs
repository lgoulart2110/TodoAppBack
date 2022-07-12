using System.Net;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Domain.Models;

namespace TodoApp.Application.Services
{
    public class NotificationService : INotificationService
    {
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        public bool HasNotifications => _notifications.Any();

        private readonly List<Notification> _notifications;

        public NotificationService()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(HttpStatusCode statusCode, string key, string message)
        {
            AddNotification(new Notification(statusCode, key, message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}
