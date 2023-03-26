using AutoMapper;
using ContactAPI.Entities;
using ContactAPI.Exceptions;
using ContactAPI.Models;

namespace ContactAPI.Services
{
    public interface IContactService
    {
        List<ContactDto> GetAll(bool isAuthenticated);
        ContactDto GetById(int id);
        int Create(CreateContactDto dto);
        void Update(int id, UpdateContactDto dto);
        void Delete(int id);
    }

    public class ContactService : IContactService
    {
        private readonly ContactDbContext _dbContext;
        private readonly IMapper _mapper;

        public ContactService(ContactDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ContactDto> GetAll(bool isAuthenticated)
        {
            var contacts = new List<Contact>();

            if (isAuthenticated)
            {
                contacts.AddRange(_dbContext.Contacts.ToList());
            }
            else
            {
                contacts.AddRange(_dbContext.Contacts.Select(c => new Contact()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                    Category = c.Category,
                    Subcategory = c.Subcategory
                }).ToList());
            }

            var contactsDto = _mapper.Map<List<ContactDto>>(contacts).OrderBy(c => c.Id).ToList();

            return contactsDto;
        }

        public ContactDto GetById(int id)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
                throw new NotFoundException("Contact not found");

            var contactsDto = _mapper.Map<ContactDto>(contact);
            return contactsDto;
        }

        public int Create(CreateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();

            return contact.Id;
        }

        public void Update(int id, UpdateContactDto dto)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
                throw new NotFoundException("Contact not found");

            contact.Email = dto.Email;
            contact.Phone = dto.Phone;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
                throw new NotFoundException("Contact not found");

            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();
        }
    }
}
