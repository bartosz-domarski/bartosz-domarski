using Gradebook.ConsoleApp.CQRS.Commands.Gradebook;
using Gradebook.ConsoleApp.CQRS.Commands.Grade;
using Gradebook.ConsoleApp.CQRS.Queries.Gradebook;
using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Notifications;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using Gradebook.ConsoleApp.Entities.AverageStrategy;

namespace Gradebook.ConsoleApp.Services
{
    public class ClientManagerService : IClientManagerService
    {
        private readonly Notifications.INotificationPublisher _publisher;
        private readonly IMediator _mediator;

        public ClientManagerService(Notifications.INotificationPublisher publisher, IMediator mediator)
        {
            _publisher = publisher;
            _mediator = mediator;
        }

        public string GetChoice()
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

        public async Task AddGradebook()
        {
            var studentName = InsertStudentName();
            var studentGender = GetStudentGender();

            if (!studentName.IsNullOrEmpty() && (studentGender == 'F' || studentGender == 'M'))
            {
                var gradebooks = _mediator.Send(new GetAllGradebooksQuery());
                var isGradebookExist = gradebooks.Result.Any(x => x.Student.Name == studentName);

                if (!isGradebookExist)
                {
                    var gradebook = new CreateGradebookCommand
                    {
                        Student = new Student
                        {
                            Name = studentName,
                            Gender = studentGender
                        }
                    };

                    await _mediator.Send(gradebook);

                    _publisher.Notify(NotificationType.Info,
                        $"Gradebook for {gradebook.Student.Name} has been created.");
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"Gradebook for {studentName} already exist.");
                }
            }
            else
            {
                _publisher.Notify(NotificationType.Error, "Input is not correct.");
            }
        }

        public async Task AddGrade()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();
                var subject = GetSubject();
                var category = GetCategory();
                var score = InsertScore();

                if (score >= 0 && score <= 100)
                {
                    var gradebook = await GetGradebook(studentName);

                    if (gradebook != null)
                    {
                        var grade = new Grade
                        {
                            Subject = subject,
                            Category = category,
                            Score = score,
                            Weight = Grade.WEIGHT[category]
                        };

                        var gradeToCreate = new CreateGradeCommand
                        {
                            Gradebook = gradebook,
                            Grade = grade
                        };

                        await _mediator.Send(gradeToCreate);

                        _publisher.Notify(NotificationType.Info,
                            $"Grade for {studentName} has been added.");

                    }
                    else
                    {
                        _publisher.Notify(NotificationType.Error,
                            $"There is no gradebook for {studentName}.");
                    }
                }
                else
                {
                    _publisher.Notify(NotificationType.Error, $"Input is not correct.");
                }
            }
        }

        public async Task PrintAllGradebooks()
        {
            var gradebooks = await _mediator.Send(new GetAllGradebooksQuery());

            if (gradebooks.Any())
            {
                foreach (var gradebook in gradebooks)
                {
                    Console.WriteLine($"{gradebook.Student.Name} | Total grades: {gradebook.Grades.Count}");
                }

                Console.ReadKey();
            }
            else
            {
                _publisher.Notify(NotificationType.Warning,
                    "There are no gradebooks.");
            }
        }

        public async Task PrintAllGradesByStudentName()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();

                var gradebook = await GetGradebook(studentName);

                if (gradebook != null)
                {
                    if (gradebook.Grades.Any())
                    {
                        foreach (var grade in gradebook.Grades)
                        {
                            Console.WriteLine($"{grade.Subject} | {grade.Category} | {grade.Score} ({grade.Weight})");
                        }

                        Console.ReadKey();
                    }
                    else
                    {
                        _publisher.Notify(NotificationType.Warning,
                            $"There are no grades for {studentName}.");
                    }
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for {studentName}.");
                }
            }
        }

        public async Task PrintGradebookDetails()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();

                var gradebook = await GetGradebook(studentName);

                if (gradebook != null)
                {
                    var totalSubjects = gradebook.Grades.Select(x => x.Subject).Distinct().Count();
                    var totalCategories = gradebook.Grades.Select(x => x.Category).Distinct().Count();

                    Console.WriteLine($"Student: {studentName} \n" +
                        $"Total subjects: {totalSubjects} \n" +
                        $"Total categories: {totalCategories} \n" +
                        $"Arithmetic average by all subjects: {ArithmeticAverage(gradebook.Grades)} \n" +
                        $"Weighted average by all subjects: {WeightedAverage(gradebook.Grades)}");

                    if (!gradebook.Grades.Any())
                    {
                        _publisher.Notify(NotificationType.Warning,
                            $"There are no grades for {studentName}.");
                    }

                    Console.ReadKey();
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for {studentName}.");
                }
            }
        }

        public async Task PrintGradeDetailsBySubject()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();
                var subject = GetSubject();

                var gradebook = await GetGradebook(studentName);

                if (gradebook != null)
                {
                    var gradesBySubject = gradebook.Grades.Where(x => x.Subject == subject).ToList();

                    foreach (var grade in gradesBySubject)
                    {
                        Console.WriteLine($"{grade.Category} | {grade.Score} ({grade.Weight})");
                    }

                    Console.WriteLine($"\nTotal grades: {gradesBySubject.Count} \n" +
                        $"Arithmetic average: {ArithmeticAverage(gradesBySubject)} \n" +
                        $"Weighted average: {WeightedAverage(gradesBySubject)}");

                    if (!gradebook.Grades.Any())
                    {
                        _publisher.Notify(NotificationType.Warning,
                            $"There are no grades for {studentName} in {subject}.");
                    }

                    Console.ReadKey();
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for {studentName}.");
                }
            }
        }

        public async Task DeleteGradebook()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();

                var gradebook = (DeleteGradebookCommand)await GetGradebook(studentName);

                if (gradebook != null)
                {
                    await _mediator.Send(gradebook);

                    _publisher.Notify(NotificationType.Info,
                        $"Gradebook for {studentName} has been removed.");
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for {studentName}.");
                }
            }
        }

        public async Task DeleteGrade()
        {
            if (await IsAnyGradebook())
            {
                var studentName = await GetStudentName();
                var subject = GetSubject();
                var category = GetCategory();
                var score = InsertScore();

                var gradebook = await GetGradebook(studentName);

                if (gradebook != null)
                {
                    var grade = gradebook.Grades.Where(x => x.Subject == subject &&
                    x.Category == category &&
                    x.Score == score).FirstOrDefault();

                    if (grade != null)
                    {
                        var gradeToDelete = new DeleteGradeCommand
                        {
                            Gradebook = gradebook,
                            Grade = grade
                        };

                        await _mediator.Send(gradeToDelete);

                        _publisher.Notify(NotificationType.Info,
                            $"Grade for {studentName} has been removed.");
                    }
                    else
                    {
                        _publisher.Notify(NotificationType.Error,
                            $"There is no grade for {studentName}.");
                    }
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for {studentName}.");
                }
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

        private async Task<string> GetStudentName()
        {
            Console.Clear();
            Console.WriteLine("Choose student");

            var studentNames = (await GetAllGradebooks()).Select(x => x.Student.Name).ToArray();

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

        private static float ArithmeticAverage(List<Grade> grades) =>
            new Average(new ArithmeticAverage())
                .Calculate(
                    grades.Select(x => x.Score),
                    grades.Select(x => x.Weight)
                );

        private static float WeightedAverage(List<Grade> grades) =>
            new Average(new WeightedAverage())
                .Calculate(
                    grades.Select(x => x.Score),
                    grades.Select(x => x.Weight)
                );

        private async Task<IEnumerable<Entities.Gradebook>> GetAllGradebooks() =>
            await _mediator.Send(new GetAllGradebooksQuery());

        private async Task<Entities.Gradebook> GetGradebook(string studentName) =>
            await _mediator.Send(new GetGradebookQuery { StudentName = studentName });

        private async Task<bool> IsAnyGradebook() =>
            await _mediator.Send(new IsAnyGradebookQuery());
    }
}
