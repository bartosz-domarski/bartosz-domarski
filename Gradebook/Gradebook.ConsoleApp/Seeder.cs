using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Persistence;

namespace Gradebook.ConsoleApp
{
    public class Seeder
    {
        private readonly IGradebookDbContext _dbContext;

        public Seeder(IGradebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Gradebooks.Any())
            {
                _dbContext.Gradebooks.Add(
                    new Entities.Gradebook
                    {
                        Student = new Student
                        {
                            Name = "John Smith",
                            Gender = 'M'
                        },
                        Grades = new List<Grade>
                        {
                            new Grade
                            {
                                Subject = Subject.Mathematics,
                                Category = Category.Test,
                                Score = 88.5f,
                                Weight = Grade.WEIGHT[Category.Test]
                            },
                            new Grade
                            {
                                Subject = Subject.Music,
                                Category = Category.Quiz,
                                Score = 78.5f,
                                Weight = Grade.WEIGHT[Category.Quiz]
                            }
                        }
                    });

                _dbContext.Gradebooks.Add(
                   new Entities.Gradebook
                   {
                       Student = new Student
                       {
                           Name = "Martha Waters",
                           Gender = 'F'
                       },
                       Grades = new List<Grade>
                       {
                            new Grade
                            {
                                Subject = Subject.ComputerScience,
                                Category = Category.Participation,
                                Score = 91.0f,
                                Weight = Grade.WEIGHT[Category.Participation]
                            },
                            new Grade
                            {
                                Subject = Subject.Geography,
                                Category = Category.GroupProject,
                                Score = 69.5f,
                                Weight = Grade.WEIGHT[Category.GroupProject]
                            }
                       }
                   });

                _dbContext.SaveChanges();
            }
        }
    }
}
