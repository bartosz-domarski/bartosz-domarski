using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Entities.AverageStrategy;
using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Gradebook.ConsoleApp.Services
{
    public class GradebookService : IGradebookService
    {
        private readonly IGradebookRepository _repository;

        private readonly INotificationPublisher _publisher;

        public GradebookService(IGradebookRepository repository, INotificationPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public void AddGradebook(string studentName, char studentGender)
        {
            if (!studentName.IsNullOrEmpty() && (studentGender == 'F' || studentGender == 'M'))
            {
                var isGradebookExist = _repository.GetAllGradebooks().Any(x => x.Student.Name == studentName);

                if (!isGradebookExist)
                {
                    var gradebook = new Entities.Gradebook()
                    {
                        Student = new Student()
                        {
                            Name = studentName,
                            Gender = studentGender
                        }
                    };

                    _repository.AddGradebook(gradebook);

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

        public void AddGrade(string studentName, Subject subject, Category category, float score)
        {
            if (score >= 0 && score <= 100)
            {
                var gradebook = _repository.GetGradebook(studentName);

                if (gradebook != null)
                {
                    var grade = new Grade()
                    {
                        Subject = subject,
                        Category = category,
                        Score = score,
                        Weight = Grade.WEIGHT[category]
                    };

                    _repository.AddGrade(gradebook, grade);

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

        public void PrintAllGradebooks()
        {
            var gradebooks = _repository.GetAllGradebooks();

            if (gradebooks.Any())
            {
                foreach (var gradebook in gradebooks)
                {
                    Console.WriteLine($"{gradebook.Student.Name} | Total grades: {gradebook.Grades.Count}");
                }
            }
            else
            {
                _publisher.Notify(NotificationType.Warning,
                    "There are no gradebooks.");
            }
        }

        public int PrintAllGradesByStudentName(string studentName)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                if (gradebook.Grades.Any())
                {
                    foreach (var grade in gradebook.Grades)
                    {
                        Console.WriteLine($"{grade.Subject} | {grade.Category} | {grade.Score} ({grade.Weight})");
                    }

                    return gradebook.Grades.Count;
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

            return 0;
        }

        public void PrintGradebookDetails(string studentName)
        {
            var gradebook = _repository.GetGradebook(studentName);

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
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for {studentName}.");
            }
        }

        public void PrintGradeDetailsBySubject(string studentName, Subject subject)
        {
            var gradebook = _repository.GetGradebook(studentName);

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
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for {studentName}.");
            }
        }

        public void DeleteGradebook(string studentName)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                _repository.DeleteGradebook(gradebook);

                _publisher.Notify(NotificationType.Info,
                    $"Gradebook for {studentName} has been removed.");
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for {studentName}.");
            }
        }

        public void DeleteGrade(string studentName, Subject subject, Category category, float score)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                var grade = gradebook.Grades.Where(x => x.Subject == subject &&
                x.Category == category &&
                x.Score == score).FirstOrDefault();

                if (grade != null)
                {
                    _repository.DeleteGrade(gradebook, grade);

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

        public bool IsAnyGradebook()
        {
            var isAnyGradebook = _repository.GetAllGradebooks().Any();

            if (!isAnyGradebook)
            {
                _publisher.Notify(NotificationType.Warning,
                        $"There are no gradebooks.");
            }

            return isAnyGradebook;
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
    }
}
