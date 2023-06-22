using Gradebook.Entities;
using Gradebook.Repositories;
using Spectre.Console;

namespace Gradebook.Services
{
    public class ClientInterfaceService : IClientInterfaceService
    {
        private readonly IGradebookRepository _gradebookRepository;
        private readonly IGradebookService _gradebookService;

        public ClientInterfaceService(IGradebookRepository gradebookRepository, IGradebookService gradebookService)
        {
            _gradebookRepository = gradebookRepository;
            _gradebookService = gradebookService;
        }

        public static string GetOption()
        {
            Console.Clear();

            return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following options:")
                .PageSize(10)
                .AddChoices(new[] {
                    "Add gradebook",
                    "Add grade",
                    "Display gradebooks",
                    "Display student's grades",
                    "Display student's gradebook details",
                    "Display student's grade details by subject",
                    "Remove gradebook",
                    "Remove grade",
                    "Exit"
                }));
        }

        public void AddGradebook()
        {
            var studentName = InsertStudentName();
            var studentGender = GetStudentGender();

            _gradebookService.AddGradebook(studentName, studentGender);
        }

        public void AddGrade()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();
                var subject = GetSubject();
                var category = GetCategory();
                var score = InsertScore();

                _gradebookService.AddGrade(studentName, subject, category, score);
            }
        }

        public void PrintAllGradebooks()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                _gradebookService.PrintAllGradebooks();
                Console.ReadKey();
            }
        }

        public void PrintAllGradesByStudentName()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();

                _gradebookService.PrintAllGradesByStudentName(studentName);
                Console.ReadKey();
            }
        }

        public void PrintGradebookDetails()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();

                _gradebookService.PrintGradebookDetails(studentName);
                Console.ReadKey();
            }
        }

        public void PrintGradeDetailsBySubject()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();
                var subject = GetSubject();

                _gradebookService.PrintGradeDetailsBySubject(studentName, subject);
                Console.ReadKey();
            }
        }

        public void DeleteGradebook()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();

                _gradebookService.DeleteGradebook(studentName);
            }
        }

        public void DeleteGrade()
        {
            if (_gradebookService.IsAnyGradebook())
            {
                var studentName = GetStudentName();
                var subject = GetSubject();
                var category = GetCategory();
                var score = InsertScore();

                _gradebookService.DeleteGrade(studentName, subject, category, score);
            }
        }

        private static string InsertStudentName()
        {
            Console.Clear();
            Console.WriteLine("Insert student's full name");

            var studentName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(studentName))
            {
                return studentName;
            }

            return string.Empty;
        }

        private string GetStudentName()
        {
            Console.Clear();
            Console.WriteLine("Choose student");

            var studentNames = _gradebookRepository.GetAllGradebooks().Select(x => x.Student.Name).ToArray();

            return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following options:")
                .PageSize(10)
                .AddChoices(studentNames));
        }

        private static char GetStudentGender()
        {
            Console.Clear();
            Console.WriteLine("Choose student's gender");

            return AnsiConsole.Prompt(
            new SelectionPrompt<char>()
                .Title("Select one of the following options:")
                .PageSize(10)
                .AddChoices('F', 'M'));
        }

        private static Subject GetSubject()
        {
            Console.Clear();
            Console.WriteLine("Choose subject");

            var subject = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following options:")
                .PageSize(10)
                .AddChoices(
                    Enum.GetNames(typeof(Subject))
                ));

            return (Subject)Enum.Parse(typeof(Subject), subject);
        }

        private static Category GetCategory()
        {
            Console.Clear();
            Console.WriteLine("Choose category");

            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following options:")
                .PageSize(10)
                .AddChoices(
                    Enum.GetNames(typeof(Category))
                ));

            return (Category)Enum.Parse(typeof(Category), category);
        }

        private static float InsertScore()
        {
            Console.Clear();
            Console.WriteLine("Insert grade");

            var input = Console.ReadLine();

            if (float.TryParse(input, out float score))
            {
                return score;
            }
            else
            {
                return 0;
            }
        }
    }
}
