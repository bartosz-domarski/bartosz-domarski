// Ignore Spelling: CBM Dto Dtos

using FiberFresh.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FiberFresh.Application.Dtos
{
    public class ServiceDto
    {
        [Required]
        public Furniture Furniture { get; set; }

        [Required]
        public Fabric Fabric { get; set; }

        [Range(0f, 50.0f)]
        public float CBM { get; set; }

        public Size Size { get; set; }

        public List<ImageDto> Images { get; set; } = default!;
    }
}
