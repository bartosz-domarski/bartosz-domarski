namespace Gradebook.Services
{
    public interface IClientInterfaceService
    {
        void AddGrade();
        void AddGradebook();
        void DeleteGrade();
        void DeleteGradebook();
        void PrintAllGradebooks();
        void PrintAllGradesByStudentName();
        void PrintGradebookDetails();
        void PrintGradeDetailsBySubject();
    }
}