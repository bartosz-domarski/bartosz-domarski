using Gradebook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Persistence
{
    public interface IGradebookDbContext
    {
        DbSet<Entities.Gradebook> Gradebooks { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Student> Students { get; set; }
        void SaveChanges();
    }
}