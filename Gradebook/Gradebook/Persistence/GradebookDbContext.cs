using Gradebook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Persistence
{
    public class GradebookDbContext : DbContext, IGradebookDbContext
    {
        public DbSet<Entities.Gradebook> Gradebooks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Gradebook>()
                .HasOne(x => x.Student)
                .WithOne(x => x.Gradebook)
                .HasForeignKey<Student>(x => x.GradebookId);

            modelBuilder.Entity<Entities.Gradebook>()
                .HasMany(x => x.Grades)
                .WithOne(x => x.Gradebook)
                .HasForeignKey(x => x.GradebookId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GradebookDb;Trusted_Connection=True;");
        }

        void IGradebookDbContext.SaveChanges() =>
            base.SaveChanges();
    }
}
