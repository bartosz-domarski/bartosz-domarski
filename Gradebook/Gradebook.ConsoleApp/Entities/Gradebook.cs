namespace Gradebook.ConsoleApp.Entities
{
    public class Gradebook
    {
        public Guid Id { get; private set; }

        public Student Student { get; set; } = default!;

        public List<Grade> Grades = new();
    }
}
