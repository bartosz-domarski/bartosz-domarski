using Contacts.Data;
using Contacts.ViewModels;

namespace Contacts
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ContactDataContext>();
            context.Database.EnsureCreated();
            AddContact(context);
        }

        private static void AddContact(ContactDataContext context)
        {
            var contact = context.Contacts.FirstOrDefault();
            if (contact != null) return;

            context.Contacts.Add(new Contact
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jankowalski@gmail.com",
                Password = "Password",
                DateOfBirth = DateTime.UtcNow,
                PhoneNumber = 987654321
            });

            context.SaveChanges();
        }
    }
}
