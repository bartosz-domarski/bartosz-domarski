using ContactsWebAPI.EFCore;
using ContactsWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsWebAPI.Controllers
{
    [ApiController]
    public class ContactsAPIController : ControllerBase
    {
        private readonly DbHelper _db;

        public ContactsAPIController(EFDataContext eFDataContext)
        {
            _db = new DbHelper(eFDataContext);
        }

        // GET: api/<ContactsAPIController>
        [HttpGet]
        [Route("api/[controller]/GetContacts")]
        public IActionResult Get()
        {
            var type = ResponseType.Success;
            try
            {
                var data = _db.GetContacts();
                if (!data.Any()) 
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ContactsAPIController>/5
        [HttpGet]
        [Route("api/[controller]/GetContactById/{id}")]
        public IActionResult Get(int id)
        {
            var type = ResponseType.Success;
            try
            {
                var data = _db.GetContactsById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<ContactsAPIController>
        [HttpPost]
        [Route("api/[controller]/SaveContact")]
        public IActionResult Post([FromBody] ContactModel contactModel)
        {
            try
            {
                var type = ResponseType.Success;
                _db.saveContact(contactModel);
                return Ok(ResponseHandler.GetAppResponse(type, contactModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<ContactsAPIController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateContact")]
        public IActionResult Put(int id, [FromBody] ContactModel contactModel)
        {
            try
            {
                var type = ResponseType.Success;
                _db.saveContact(contactModel);
                return Ok(ResponseHandler.GetAppResponse(type, contactModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<ContactsAPIController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteContact/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var type = ResponseType.Success;
                _db.deleteContact(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
