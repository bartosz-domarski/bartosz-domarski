using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Persistence;
using Gradebook.ConsoleApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Gradebook.Tests
{
    public class GradebookRepositoryTests
    {
        private readonly IGradebookDbContext _dbContext;
        private readonly IGradebookRepository _repository;

        public GradebookRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<GradebookDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;

            _dbContext = new GradebookDbContext(options);
            _repository = new GradebookRepository(_dbContext);
        }

        [Fact]
        public void GetGradebook_ForCorrectStudentName_ReturnsGradebook()
        {
            //arrange
            var studentName = "test";
            var gradebook = new ConsoleApp.Entities.Gradebook 
            { 
                Student = new Student 
                { 
                    Name = studentName 
                } 
            };

            _repository.AddGradebook(gradebook);


            //act
            var result = _repository.GetGradebook(studentName);

            //assert
            Assert.Equal(studentName, result.Student.Name);
        }

        [Fact]
        public void GetAllGradebooks_ChecksIfReturnsAllGradebooks()
        {
            //arrange
            var studentName = "test2";
            var studentName2 = "test3";

            var gradebook = new ConsoleApp.Entities.Gradebook
            {
                Student = new Student
                {
                    Name = studentName
                }
            };

            var gradebook2 = new ConsoleApp.Entities.Gradebook
            {
                Student = new Student
                {
                    Name = studentName2
                }
            }; 

            var count = _dbContext.Gradebooks.Count();

            _repository.AddGradebook(gradebook);
            _repository.AddGradebook(gradebook2);

            //act
            var result = _repository.GetAllGradebooks();

            //assert
            Assert.Equal(count + 2, _dbContext.Gradebooks.Count());
            Assert.Equal(studentName, result[^2].Student.Name);
            Assert.Equal(studentName2, result[^1].Student.Name);
        }
    }
}
