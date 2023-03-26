using System.ComponentModel;

namespace ContactAPI.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Category { get; set; }
        public string? Subcategory { get; set; }

        public static readonly Dictionary<string, string[]> SubcategoriesAvailable =
            new Dictionary<string, string[]>() { { "business", new[] { "client", "boss", "other" } } };
    }
}
