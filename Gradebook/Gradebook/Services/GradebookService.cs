using Gradebook.Entities;
using Gradebook.Entities.AverageStrategy;
using Gradebook.Notifications;
using Gradebook.Repositories;

namespace Gradebook.Services
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
            var gradebook = new Entities.Gradebook()
            {
                Student = new Student()
                {
                    Name = studentName,
                    Gender = studentGender
                }
            };


            if (studentName != string.Empty)
            {
                var isGradebookExist = _repository.GetAllGradebooks().Any(x => x.Student.Name == gradebook.Student.Name);

                if (!isGradebookExist)
                {
                    _repository.AddGradebook(gradebook);

                    _publisher.Notify(NotificationType.Info,
                        $"Gradebook for student {gradebook.Student.Name} has been created.");
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"Gradebook for student {gradebook.Student.Name} already exist.");
                }
            }
            else
            {
                _publisher.Notify(NotificationType.Error, "Input is not correct.");
            }
        }

        public void AddGrade(string studentName, Subject subject, Category category, float score)
        {
            if (score > 0)
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
                        $"Grade for student {studentName} has been added.");

                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no gradebook for student {studentName}.");
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

            if (gradebooks.Count > 0)
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

        public void PrintAllGradesByStudentName(string studentName)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                if (gradebook.Grades.Count > 0)
                {
                    foreach (var grade in gradebook.Grades)
                    {
                        Console.WriteLine($"{grade.Subject} | {grade.Category} | {grade.Score} ({grade.Weight})");
                    }
                }
                else
                {
                    _publisher.Notify(NotificationType.Warning,
                        $"There are no grades for student {studentName}.");
                }
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for student {studentName}.");
            }
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
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for student {studentName}.");
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
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for student {studentName}.");
            }
        }

        public void DeleteGradebook(string studentName)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                _repository.DeleteGradebook(gradebook);

                _publisher.Notify(NotificationType.Info,
                    $"Gradebook for student {studentName} has been removed.");
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for student {studentName}.");
            }
        }

        public void DeleteGrade(string studentName, Subject subject, Category category, float score)
        {
            var gradebook = _repository.GetGradebook(studentName);

            if (gradebook != null)
            {
                var grade = gradebook.Grades.FirstOrDefault(x => x.Subject == subject &&
                x.Category == category &&
                x.Score == score);

                if (grade != null)
                {
                    _repository.DeleteGrade(gradebook, grade);

                    _publisher.Notify(NotificationType.Info,
                        $"Grade for student {studentName} has been removed.");
                }
                else
                {
                    _publisher.Notify(NotificationType.Error,
                        $"There is no grade for student {studentName}.");
                }
            }
            else
            {
                _publisher.Notify(NotificationType.Error,
                    $"There is no gradebook for student {studentName}.");
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
