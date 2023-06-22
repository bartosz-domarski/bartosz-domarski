namespace Gradebook.Entities
{
    public class Gradebook
    {
        public Guid Id { get; set; }
        public Student Student { get; set; } = default!;
        public List<Grade> Grades { get; set; }
        public Gradebook()
        {
            Grades = new List<Grade>();
        }
    }
}
