// Ignore Spelling: Dto Dtos

using Microsoft.AspNetCore.Http;

namespace FiberFresh.Application.Dtos
{
    public class ImageDto
    {
        public string Name { get; set; } = default!;

        public IFormFile Blob { get; set; } = default!;
    }
}
