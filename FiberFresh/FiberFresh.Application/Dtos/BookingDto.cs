// Ignore Spelling: Dto Dtos

using FiberFresh.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FiberFresh.Application.Dtos
{
    public class BookingDto
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; } = default!;

        [EmailAddress]
        public string? Email { get; set; } = default!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        public string City { get; set; } = default!;

        [Required]
        public string Street { get; set; } = default!;

        [Required]
        [Range(-3, 12)]
        public int Floor { get; set; }

        [Required]
        public bool IsElevator { get; set; }

        [Required]
        public List<ServiceDto> Services { get; set; } = default!;

        [Required]
        public DateOnly DateOfService { get; set; } = default!;

        [Required]
        public TimeOfDay TimeOfDay { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
