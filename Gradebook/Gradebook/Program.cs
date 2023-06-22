using Gradebook;
using Gradebook.Notifications;
using Gradebook.Persistence;
using Gradebook.Repositories;
using Gradebook.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IGradebookDbContext, GradebookDbContext>();
services.AddSingleton<IGradebookRepository, GradebookRepository>();
services.AddSingleton<IGradebookService, GradebookService>();
services.AddSingleton<IClientInterfaceService, ClientInterfaceService>();
services.AddSingleton<INotification, Notification>();
services.AddSingleton<INotificationPublisher, NotificationPublisher>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<IApp>();

app.Run();