using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Models
{
    public class CreateContactDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public string? Subcategory { get; set; }
    }
}
