using ContactAPI.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace ContactAPI
{
    public class ContactSeeder
    {
        private readonly ContactDbContext _dbContext;

        public ContactSeeder(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Contacts.Any())
                {
                    var contacts = GetContacts();
                    _dbContext.Contacts.AddRange(contacts);
                    _dbContext.SaveChanges();
                }
            }
        }

        private List<Contact> GetContacts()
        {
            var contacts = new List<Contact>()
            {
                new Contact()
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jankowalski@gmail.com",
                    Phone = "123456789",
                    DateOfBirth = DateTime.SpecifyKind(DateTime.Parse("1990-01-01"), DateTimeKind.Utc),
                    Password = "Password1!",
                    Category = "Barber"
                },
                new Contact()
                {
                    FirstName = "Paweł",
                    LastName = "Nowak",
                    Email = "pawelnowak@gmail.com",
                    Phone = "987654321",
                    DateOfBirth = DateTime.SpecifyKind(DateTime.Parse("1991-01-01"), DateTimeKind.Utc),
                    Password = "passworD2.",
                    Category = "Private"
                },
                new Contact()
                {
                    FirstName = "Anna",
                    LastName = "Duda",
                    Email = "annaduda@gmail.com",
                    Phone = "123123123",
                    DateOfBirth = DateTime.SpecifyKind(DateTime.Parse("1992-01-01"), DateTimeKind.Utc),
                    Password = "paSSword?3",
                    Category = "Business",
                    Subcategory = "Boss"
                }
            };
            return contacts;
        }
    }
}
