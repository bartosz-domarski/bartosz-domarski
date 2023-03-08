namespace PhoneBook
{
    class PhoneBook
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public void AddContact(Contact contact)
        {
            if (contact.Name != null && contact.PhoneNumber != null)
            {
                Contacts.Add(contact);
            }
        }

        private void ShowContactDetails(Contact contact)
        {
            Console.WriteLine($"Contact: {contact.Name}, {contact.PhoneNumber}");
        }

        private void ShowContactsDetails(List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                ShowContactDetails(contact);
            }
        }

        public void ShowContactByNumber(string number)
        {
            var contact = Contacts.FirstOrDefault(x => x.PhoneNumber == number);

            if (contact != null)
            {
                ShowContactDetails(contact);
            }
            else
            {
                Console.WriteLine("Contact not found");
            }
        }

        public void ShowAllContacts()
        {
            ShowContactsDetails(Contacts);
        }

        public void SearchforContactsByName(string pharse)
        {
            var contacts = Contacts.Where(c => c.Name.Contains(pharse)).ToList();

            ShowContactsDetails(contacts);
        }

        public void RemoveContactByNumber(string number)
        {
            Contacts.Remove(Contacts.Where(c => c.PhoneNumber == number).First());
        }
    }
}