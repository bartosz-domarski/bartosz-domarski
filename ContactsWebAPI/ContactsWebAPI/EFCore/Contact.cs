using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsWebAPI.EFCore
{
    [Table("contact")]
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Hasło musi zawierać co najmniej 8 znaków, w tym co najmniej jedną literę, jedną cyfrę i jeden znak specjalny.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public DateOnly DateOfBirth { get; set; }
    }
}
