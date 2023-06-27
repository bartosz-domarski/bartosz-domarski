namespace Gradebook.ConsoleApp.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }
        public Guid GradebookId { get; private set; }
        public Gradebook Gradebook { get; private set; } = default!;
        public string Name { get; set; } = default!;
        public char Gender { get; set; }
    }
}
