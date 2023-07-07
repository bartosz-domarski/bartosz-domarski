using Gradebook.ConsoleApp;
using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Persistence;
using Gradebook.ConsoleApp.Repositories;
using Gradebook.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
services.AddDbContext<IGradebookDbContext, GradebookDbContext>();
services.AddSingleton<IGradebookRepository, GradebookRepository>();
services.AddSingleton<IClientManagerService, ClientManagerService>();
services.AddSingleton<INotification, Notification>();
services.AddSingleton<INotificationPublisher, NotificationPublisher>();
services.AddSingleton<Seeder>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<IApp>();

var seeder = serviceProvider.GetRequiredService<Seeder>();
seeder.Seed();

await app.Run();