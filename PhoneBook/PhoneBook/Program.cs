namespace PhoneBook
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 Create new contact");
            Console.WriteLine("2 Display all contacts");
            Console.WriteLine("3 Display contact by phone number");
            Console.WriteLine("4 Display contact by name");
            Console.WriteLine("5 Delete contact by phone number");
            Console.WriteLine("x Exit the program");

            var phoneBook = new PhoneBook();

            Console.WriteLine("Input operation number");
            string userInput = Console.ReadLine();

            while (true)
            {
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Input name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Input phone number");
                        string number = Console.ReadLine();
                        var contact = new Contact(name, number);
                        phoneBook.AddContact(contact);
                        break;
                    case "2":
                        phoneBook.ShowAllContacts();
                        break;
                    case "3":
                        Console.WriteLine("Input phone number");
                        string numberToSerach = Console.ReadLine();
                        phoneBook.ShowContactByNumber(numberToSerach);
                        break;
                    case "4":
                        Console.WriteLine("Input name");
                        string nameToSearch = Console.ReadLine();
                        phoneBook.SearchforContactsByName(nameToSearch);
                        break;
                    case "5":
                        Console.WriteLine("Input phone number");
                        string numberToDelete = Console.ReadLine();
                        phoneBook.RemoveContactByNumber(numberToDelete);
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
                Console.WriteLine("Input operation number");
                userInput = Console.ReadLine();
            }
        }
    }
}