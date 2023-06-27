using Gradebook.ConsoleApp;
using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Persistence;
using Gradebook.ConsoleApp.Repositories;
using Gradebook.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddDbContext<IGradebookDbContext, GradebookDbContext>();
services.AddSingleton<IGradebookRepository, GradebookRepository>();
services.AddSingleton<IGradebookService, GradebookService>();
services.AddSingleton<IClientInterfaceService, ClientInterfaceService>();
services.AddSingleton<INotification, Notification>();
services.AddSingleton<INotificationPublisher, NotificationPublisher>();
services.AddSingleton<Seeder>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<IApp>();

var seeder = serviceProvider.GetRequiredService<Seeder>();
seeder.Seed();

app.Run();