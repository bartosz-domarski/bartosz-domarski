namespace Gradebook.ConsoleApp.Notifications
{
    public interface INotificationPublisher
    {
        void Notify(NotificationType type, string context);
        void Subscribe(INotification notification);
        void Unsubscribe(INotification notification);
    }
}