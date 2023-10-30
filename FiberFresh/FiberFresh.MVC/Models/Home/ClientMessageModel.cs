// Ignore Spelling: MVC

using System.ComponentModel.DataAnnotations;

namespace FiberFresh.MVC.Models.Home
{
    public class ClientMessageModel
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Message { get; set; } = default!;
    }
}
