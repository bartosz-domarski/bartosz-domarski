using Microsoft.EntityFrameworkCore;

namespace ContactsWebAPI.EFCore
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
