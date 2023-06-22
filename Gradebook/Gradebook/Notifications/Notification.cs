using Microsoft.Toolkit.Uwp.Notifications;

namespace Gradebook.Notifications
{
    public class Notification : INotification
    {
        public void Update(NotificationType type, string context)
        {
            new ToastContentBuilder()
                .AddAppLogoOverride(new Uri($"{Environment.CurrentDirectory}\\img\\{type}.png"))
                .AddText(context)
                .Show();
        }
    }
}
