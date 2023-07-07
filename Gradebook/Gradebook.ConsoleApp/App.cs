using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Services;

namespace Gradebook.ConsoleApp
{
    public class App : IApp
    {
        private readonly IClientManagerService _clientManagerService;
        private readonly INotification _notification;
        private readonly INotificationPublisher _publisher;

        public App(IClientManagerService clientManagerService, INotification notification, INotificationPublisher publisher)
        {
            _clientManagerService = clientManagerService;
            _notification = notification;
            _publisher = publisher;
        }

        public async Task Run()
        {
            _publisher.Subscribe(_notification);

            while (true)
            {
                Console.Clear();
                
                var choice = _clientManagerService.GetChoice();

                switch (choice)
                {
                    case "Add gradebook":
                        await _clientManagerService.AddGradebook();
                        break;

                    case "Add grade":
                        await _clientManagerService.AddGrade();
                        break;

                    case "Display gradebooks":
                        await _clientManagerService.PrintAllGradebooks();
                        break;

                    case "Display student's grades":
                        await _clientManagerService.PrintAllGradesByStudentName();
                        break;

                    case "Display student's gradebook details":
                        await _clientManagerService.PrintGradebookDetails();
                        break;

                    case "Display student's grade details by subject":
                        await _clientManagerService.PrintGradeDetailsBySubject();
                        break;

                    case "Remove gradebook":
                        await _clientManagerService.DeleteGradebook();
                        break;

                    case "Remove grade":
                        await _clientManagerService.DeleteGrade();
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;

                    default:
                        throw new Exception($"Option {choice} is not handled");
                }
            }
        }
    }
}
