using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.ConsoleApp.Repositories
{
    public class GradebookRepository : IGradebookRepository
    {
        private readonly IGradebookDbContext _dbContext;

        public GradebookRepository(IGradebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddGradebook(Entities.Gradebook gradebook)
        {
            _dbContext.Gradebooks.Add(gradebook);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task AddGrade(Entities.Gradebook gradebook, Grade grade)
        {
            gradebook.Grades.Add(grade);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<Entities.Gradebook> GetGradebook(string studentName) =>
            (await _dbContext.Gradebooks.FirstOrDefaultAsync(x => x.Student.Name == studentName))!;

        public async Task<IEnumerable<Entities.Gradebook>> GetAllGradebooks() =>
            await _dbContext.Gradebooks.Include(x => x.Student).Include(x => x.Grades).ToListAsync();

        public Task DeleteGradebook(Entities.Gradebook gradebook)
        {
            _dbContext.Gradebooks.Remove(gradebook);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task DeleteGrade(Entities.Gradebook gradebook, Grade grade)
        {
            gradebook.Grades.Remove(grade);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
