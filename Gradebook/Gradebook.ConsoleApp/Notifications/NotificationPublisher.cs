namespace Gradebook.ConsoleApp.Notifications
{
    public class NotificationPublisher : INotificationPublisher
    {
        private readonly List<INotification> _notifications = new();

        public void Subscribe(INotification notification)
        {
            _notifications.Add(notification);
        }

        public void Unsubscribe(INotification notification)
        {
            _notifications.Remove(notification);
        }

        public void Notify(NotificationType type, string context)
        {
            foreach (var notification in _notifications)
            {
                notification.Update(type, context);
            }
        }
    }
}
