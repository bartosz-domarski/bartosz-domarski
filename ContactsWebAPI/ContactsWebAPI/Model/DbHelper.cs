using ContactsWebAPI.EFCore;

namespace ContactsWebAPI.Model
{
    public class DbHelper
    {
        private EFDataContext _context;
        
        public DbHelper(EFDataContext context)
        {
            _context = context;
        }
        //GET
        public List<ContactModel> GetContacts()
        {
            var response = new List<ContactModel>();
            var dataList = _context.Contacts.ToList();
            dataList.ForEach(contact => response.Add(new ContactModel()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                DateOfBirth = contact.DateOfBirth,
                PhoneNumber = contact.PhoneNumber
            }));
            return response;
        }

        public ContactModel GetContactsById(int id)
        {
            var response = new ContactModel();
            var contact = _context.Contacts.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return new ContactModel()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                DateOfBirth = contact.DateOfBirth,
                PhoneNumber = contact.PhoneNumber
            };
        }

        public void saveContact(ContactModel contactModel)
        {
            Contact dbTable = new Contact();
            if (contactModel.Id > 0) 
            {
                // PUT
                dbTable = _context.Contacts.Where(x => x.Id.Equals(contactModel.Id)).FirstOrDefault();
                if (dbTable == null)
                {
                    dbTable.FirstName = contactModel.FirstName;
                    dbTable.LastName = contactModel.LastName;
                    dbTable.Email = contactModel.Email;
                    dbTable.Password = contactModel.Password;
                    dbTable.DateOfBirth = contactModel.DateOfBirth;
                    dbTable.PhoneNumber = contactModel.PhoneNumber;
                }
            }
            else
            {
                // POST
                dbTable.FirstName = contactModel.FirstName;
                dbTable.LastName = contactModel.LastName;
                dbTable.Email = contactModel.Email;
                dbTable.Password = contactModel.Password;
                dbTable.DateOfBirth = contactModel.DateOfBirth;
                dbTable.PhoneNumber = contactModel.PhoneNumber;
                _context.Contacts.Add(dbTable);
            }
            _context.SaveChanges();
        }
        //DELETE
        public void deleteContact(int id)
        {
            var contact = _context.Contacts.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (contact != null )
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
        }
    }
}
