using Gradebook.ConsoleApp.Entities;

namespace Gradebook.ConsoleApp.Repositories
{
    public interface IGradebookRepository
    {
        void AddGradebook(Entities.Gradebook gradebook);
        void AddGrade(Entities.Gradebook gradebook, Grade grade);
        List<Entities.Gradebook> GetAllGradebooks();
        Entities.Gradebook GetGradebook(string studentName);
        void DeleteGradebook(Entities.Gradebook gradebook);
        void DeleteGrade(Entities.Gradebook gradebook, Grade grade);
    }
}