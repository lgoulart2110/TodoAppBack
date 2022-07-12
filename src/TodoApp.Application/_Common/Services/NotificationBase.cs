using FluentValidation.Results;
using System.Net;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Domain._Common.Extensions;
using TodoApp.Domain.Models;

namespace TodoApp.Application._Common.Services
{
    public abstract class NotificationBase
    {
        protected readonly INotificationService _notificationService;

        protected NotificationBase(
            INotificationService notificationService
        )
        {
            _notificationService = notificationService;
        }

        protected void Notify(HttpStatusCode statusCode, string key, string message)
            => _notificationService.AddNotification(statusCode, key, message);

        protected void NotifyValidationResult(ValidationResult validationResult)
        {
            var notifications = validationResult.Errors.Select(x => new Notification(HttpStatusCode.BadRequest, x.PropertyName, x.ErrorMessage));
            _notificationService.AddNotifications(notifications);
        }

        protected void NotifyException(Exception exception)
        {
            _notificationService.AddNotification(new Notification(
                HttpStatusCode.InternalServerError, exception.GetType().Name, exception.GetFullMessageString()));
        }
    }
}
