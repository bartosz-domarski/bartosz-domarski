using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Contacts.Data
{
    public class ContactDataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactDataContext(DbContextOptions<ContactDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Password)
                    .HasConversion(
                        v => Convert.ToBase64String(Encoding.UTF8.GetBytes(v)),
                        v => Encoding.UTF8.GetString(Convert.FromBase64String(v))
                    )
                    .IsRequired();

                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
