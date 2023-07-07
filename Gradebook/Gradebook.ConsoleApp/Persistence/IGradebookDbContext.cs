using Gradebook.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.ConsoleApp.Persistence
{
    public interface IGradebookDbContext
    {
        DbSet<Entities.Gradebook> Gradebooks { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Student> Students { get; set; }
        Task SaveChanges();
    }
}