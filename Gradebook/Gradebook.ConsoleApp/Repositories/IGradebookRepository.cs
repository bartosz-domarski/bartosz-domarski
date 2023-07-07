using Gradebook.ConsoleApp.Entities;

namespace Gradebook.ConsoleApp.Repositories
{
    public interface IGradebookRepository
    {
        Task AddGradebook(Entities.Gradebook gradebook);
        Task AddGrade(Entities.Gradebook gradebook, Grade grade);
        Task<IEnumerable<Entities.Gradebook>> GetAllGradebooks();
        Task<Entities.Gradebook> GetGradebook(string studentName);
        Task DeleteGradebook(Entities.Gradebook gradebook);
        Task DeleteGrade(Entities.Gradebook gradebook, Grade grade);
    }
}