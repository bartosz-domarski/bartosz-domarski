namespace Gradebook.ConsoleApp.Services
{
    public interface IClientManagerService
    {
        string GetChoice();
        Task AddGrade();
        Task AddGradebook();
        Task DeleteGrade();
        Task DeleteGradebook();
        Task PrintAllGradebooks();
        Task PrintAllGradesByStudentName();
        Task PrintGradebookDetails();
        Task PrintGradeDetailsBySubject();
    }
}