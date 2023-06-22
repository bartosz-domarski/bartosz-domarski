using Gradebook.Notifications;
using Gradebook.Services;

namespace Gradebook
{
    public class App : IApp
    {
        private readonly IClientInterfaceService _clientInterfaceService;
        private readonly INotification _notification;
        private readonly INotificationPublisher _publisher;

        public App(IClientInterfaceService clientInterfaceService, INotification notification, INotificationPublisher publisher)
        {
            _clientInterfaceService = clientInterfaceService;
            _notification = notification;
            _publisher = publisher;
        }

        public void Run()
        {
            _publisher.Subscribe(_notification);

            while (true)
            {
                Console.Clear();

                var option = ClientInterfaceService.GetOption();

                switch (option)
                {
                    case "Add gradebook":
                        _clientInterfaceService.AddGradebook();
                        break;

                    case "Add grade":
                        _clientInterfaceService.AddGrade();
                        break;

                    case "Display gradebooks":
                        _clientInterfaceService.PrintAllGradebooks();
                        break;

                    case "Display student's grades":
                        _clientInterfaceService.PrintAllGradesByStudentName();
                        break;

                    case "Display student's gradebook details":
                        _clientInterfaceService.PrintGradebookDetails();
                        break;

                    case "Display student's grade details by subject":
                        _clientInterfaceService.PrintGradeDetailsBySubject();
                        break;

                    case "Remove gradebook":
                        _clientInterfaceService.DeleteGradebook();
                        break;

                    case "Remove grade":
                        _clientInterfaceService.DeleteGrade();
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;

                    default:
                        throw new Exception($"Option {option} is not handled");
                }
            }
        }
    }
}
