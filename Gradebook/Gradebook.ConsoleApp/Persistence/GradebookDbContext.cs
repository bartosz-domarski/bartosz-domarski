using Gradebook.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Gradebook.ConsoleApp.Persistence
{
    public class GradebookDbContext : DbContext, IGradebookDbContext
    {
        public DbSet<Entities.Gradebook> Gradebooks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public GradebookDbContext(DbContextOptions<GradebookDbContext> options) : base(options)
        {
            
        }

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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GradebookDb;Trusted_Connection=True;");
            }
        }

        async Task IGradebookDbContext.SaveChanges() =>
            await base.SaveChangesAsync();
    }
}
