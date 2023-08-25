using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects,
// visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    /// <summary>
    /// This allows operations on Contact objects
    /// </summary>
    [Route("api/contacts")]
    [ApiController]
    //[Authenticator]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository contactRepository; // ADD ME

        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _logger.LogInformation("INFO");
            _logger.LogWarning("WARNING");
            _logger.LogError("ERROR");
            this.contactRepository = contactRepository;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>List of contacts</returns>
        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult Get()
        {
            //int x = 1;
            //x = x / (x - 1);
            return Ok(contactRepository.Contacts);
        }

        [HttpGet("{contactId:int}/orders")]
        public IActionResult GetCustomerOrders(int contactId)
        {
            var contact = contactRepository.Contacts.FirstOrDefault(t => t.Id == contactId);

            if (contact == null)
            {
                return NotFound(null);
            }
            else
            {
                return Ok(contact);
            }
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // BUGBUG: don't want to load all contacts
            var contact = contactRepository.Contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound(null);
            }
            else
            {
                return Ok(contact);
            }
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            //if (string.IsNullOrEmpty(value.Name))
            //{
            //    var errorResponse = new ErrorResponse
            //    {
            //        DBCode = ErrorType.MissingField,
            //        Message = "Null or Empty Name",
            //        FieldName = "Name",
            //    };
            //    return BadRequest(errorResponse);
            //}
                        
            contactRepository.Add(value);

            //var result = new { Id = value.Id, Candy = true };

            // Look at the "Location" header in the response output in Postman
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            if (contactRepository.Update(id, value))
            {
                return Ok(value);
            }

            return NotFound(null);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = contactRepository.Delete(id);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound(null);
            }
        }
    }
}