namespace PhoneBook
{
    class Contact
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length < 3)
                {
                    Console.WriteLine("Name is too short");
                }
                else
                {
                    name = value;
                }
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value.Length < 9)
                {
                    Console.WriteLine("Number is too short");
                }
                else
                {
                    phoneNumber = value;
                }
            }
        }

        public Contact(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}