using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Persistence;
using Gradebook.ConsoleApp.Repositories;
using Gradebook.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gradebook.Tests
{
    public class GradebookServiceTests
    {
        private readonly GradebookDbContext _dbContext;
        private readonly GradebookService _service;

        public GradebookServiceTests()
        {
            var options = new DbContextOptionsBuilder<GradebookDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;

            _dbContext = new GradebookDbContext(options);
            _service = new GradebookService(new GradebookRepository(_dbContext), new NotificationPublisher());
        }

        [Fact]
        public void AddGradebook_ForUniqueStudentName_ChecksIfGradebookIsAddedOnce()
        {
            //arrange
            var studentName = "test1";
            var count = _dbContext.Gradebooks.Count();

            //act
            _service.AddGradebook(studentName, 'M');
            _service.AddGradebook(studentName, 'F');

            //assert
            Assert.True(_dbContext.Gradebooks.Any(x => x.Student.Name == studentName && x.Student.Gender == 'M'));
            Assert.False(_dbContext.Gradebooks.Any(x => x.Student.Name == studentName && x.Student.Gender == 'F'));
            Assert.Equal(count + 1, _dbContext.Gradebooks.Count());
        }

        [Fact]
        public void AddGradebook_ForInvalidStudentName_ChecksIfGradebookIsNotAdded()
        {
            //arrange
            var count = _dbContext.Gradebooks.Count();

            //act
            _service.AddGradebook(string.Empty, 'M');
            _service.AddGradebook(string.Empty, 'F');
            _service.AddGradebook("", 'M');
            _service.AddGradebook("", 'F');


            //assert
            Assert.Equal(count, _dbContext.Gradebooks.Count());
        }

        [Fact]
        public void AddGrade_ForCorrectParameters_ChecksIfGradeIsAdded()
        {
            //arrange
            var studentName = "test2";
            var score = 88.8f;
            _service.AddGradebook(studentName, 'M');

            var gradebook = _dbContext.Gradebooks.Select(x => x.Student.Name == studentName);
            var count = _dbContext.Grades.Count();

            //act
            _service.AddGrade(studentName, ConsoleApp.Entities.Subject.Mathematics, ConsoleApp.Entities.Category.Test, score);

            //assert
            Assert.True(_dbContext.Grades.Any(x => x.Gradebook.Student.Name == studentName
                && x.Subject == ConsoleApp.Entities.Subject.Mathematics && x.Category == ConsoleApp.Entities.Category.Test
                && x.Score == score));

            Assert.Equal(count + 1, _dbContext.Grades.Count());
        }

        [Fact]
        public void AddGrade_ForInvalidScore_ChecksIfGradeIsNotAdded()
        {
            //arrange
            var studentName = "test2";
            _service.AddGradebook(studentName, 'M');

            var gradebook = _dbContext.Gradebooks.Select(x => x.Student.Name == studentName);
            var count = _dbContext.Grades.Count();

            //act
            _service.AddGrade(studentName, ConsoleApp.Entities.Subject.Mathematics, ConsoleApp.Entities.Category.Test, 123);
            _service.AddGrade(studentName, ConsoleApp.Entities.Subject.Art, ConsoleApp.Entities.Category.Homework, -44.5f);

            //assert
            Assert.False(_dbContext.Grades.Any(x => x.Gradebook.Student.Name == studentName
                && x.Subject == ConsoleApp.Entities.Subject.Mathematics && x.Category == ConsoleApp.Entities.Category.Test
                && x.Score == 0));

            Assert.Equal(count, _dbContext.Grades.Count());
        }

        [Fact]
        public void DeleteGradebook_ChecksIfGradebookIsDeleted()
        {
            //arrange
            var studentName = "test3";
            var count = _dbContext.Grades.Count();

            _service.AddGradebook(studentName, 'M');
            _dbContext.Gradebooks.Select(x => x.Student.Name == studentName);

            //act
            _service.DeleteGradebook(studentName);

            //assert
            Assert.False(_dbContext.Gradebooks.Any(x => x.Student.Name == studentName));
            Assert.Equal(count, _dbContext.Grades.Count());
        }

        [Fact]
        public void DeleteGrade_ChecksIfGradeIsDeleted()
        {
            //arrange
            var studentName = "test4";
            var score = 77.7f;
            var count = _dbContext.Grades.Count();

            _service.AddGradebook(studentName, 'M');
            _service.AddGrade(studentName, ConsoleApp.Entities.Subject.ComputerScience,
                ConsoleApp.Entities.Category.Participation, score);

            //act
            _service.DeleteGrade(studentName, ConsoleApp.Entities.Subject.ComputerScience,
                ConsoleApp.Entities.Category.Participation, score);

            //assert
            Assert.False(_dbContext.Grades.Any(x => x.Gradebook.Student.Name == studentName &&
            x.Subject == ConsoleApp.Entities.Subject.ComputerScience && x.Category == ConsoleApp.Entities.Category.Participation &&
            x.Score == score));

            Assert.Equal(count, _dbContext.Grades.Count());
        }
    }
}