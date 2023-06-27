namespace Gradebook.ConsoleApp.Entities
{
    public class Grade
    {
        public static readonly Dictionary<Category, int> WEIGHT = new()
        {
            { Category.Test, 3 },
            { Category.Homework, 1},
            { Category.Project, 3},
            { Category.Participation, 1},
            { Category.Quiz, 2},
            { Category.WrittenAssignment, 2},
            { Category.GroupProject, 3}
        };

        public Guid Id { get; private set; }

        public Subject Subject { get; set; }

        public Category Category { get; set; }

        public float Score { get; set; }

        public int Weight { get; set; }

        public Guid GradebookId { get; private set; }

        public Gradebook Gradebook { get; private set; } = default!;
    }
}
