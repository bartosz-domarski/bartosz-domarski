namespace Gradebook.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid GradebookId { get; set; }
        public Gradebook Gradebook { get; set; } = default!;
        public string Name { get; set; } = default!;
        public char Gender { get; set; }
    }
}
