namespace Gradebook.Notifications
{
    public interface INotification
    {
        void Update(NotificationType type, string context);
    }
}
