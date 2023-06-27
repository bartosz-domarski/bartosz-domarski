using Gradebook.ConsoleApp.Entities;

namespace Gradebook.ConsoleApp.Services
{
    public interface IGradebookService
    {
        void AddGradebook(string studentNane, char studentGender);
        void AddGrade(string studentName, Subject subject, Category category, float score);
        void PrintAllGradebooks();
        int PrintAllGradesByStudentName(string studentName);
        void PrintGradebookDetails(string studentName);
        void PrintGradeDetailsBySubject(string studentName, Subject subject);
        void DeleteGradebook(string studentName);
        void DeleteGrade(string studentName, Subject subject, Category category, float score);
        bool IsAnyGradebook();
    }
}