using ContactAPI.Models;
using ContactAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactAPI.Controllers
{
    [Route("api/contact")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService service)
        {
            _contactService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<ContactDto>> GetAll()
        {
            var contacts = _contactService.GetAll(User.Identity.IsAuthenticated);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<ContactDto> GetById([FromRoute] int id)
        {
            var contact = _contactService.GetById(id);
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateContactDto dto)
        {
            var id = _contactService.Create(dto);
            return Created($"/api/contact/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateContactDto dto)
        {
            _contactService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _contactService.Delete(id);
            return Ok();
        }
    }
}