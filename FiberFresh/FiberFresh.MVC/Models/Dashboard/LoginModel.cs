// Ignore Spelling: MVC

using System.ComponentModel.DataAnnotations;

namespace FiberFresh.MVC.Models.Dashboard
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
