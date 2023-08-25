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
    [Header("My-Api-Key","2")]
    //[Header("Accept","application/vnd.contacts.v2+json")]
    //[Authenticator]
    public class ContactsV2Controller : ControllerBase
    {
        private static int currentId = 101;

        private static List<ContactV2> contacts = new List<ContactV2>();

        private readonly ILogger<ContactsController> _logger;

        public ContactsV2Controller(ILogger<ContactsController> logger)
        {
            _logger = logger;
            _logger.LogInformation("INFO");
            _logger.LogWarning("WARNING");
            _logger.LogError("ERROR");
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
            return Ok(contacts);
        }

        [HttpGet("{contactId:int}/orders")]
        public IActionResult GetCustomerOrders(int contactId)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == contactId);

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
            var contact = contacts.FirstOrDefault(t => t.Id == id);

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
        public IActionResult Post([FromBody] ContactV2 value)
        {
            if (string.IsNullOrEmpty(value.FirstName))
            {
                var errorResponse = new ErrorResponse
                {
                    DBCode = ErrorType.MissingField,
                    Message = "Null or Empty FirstName",
                    FieldName = "FirstName",
                };
                return BadRequest(errorResponse);
            }

            if (string.IsNullOrEmpty(value.LastName))
            {
                var errorResponse = new ErrorResponse
                {
                    DBCode = ErrorType.MissingField,
                    Message = "Null or Empty LastName",
                    FieldName = "LastName",
                };
                return BadRequest(errorResponse);
            }

            value.Id = currentId++;
            value.DateAdded = DateTime.UtcNow;
            //value.AddedByWho = "tbd";
            contacts.Add(value);

            //var result = new { Id = value.Id, Candy = true };

            // Look at the "Location" header in the response output in Postman
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactV2 value)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound(null);
            }

            // DO NOT copy the contact.ID field from value

            contact.FirstName = value.FirstName;
            contact.LastName = value.LastName;
            contact.Phones = value.Phones;

            return Ok(contact);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rowsDeleted = contacts.RemoveAll(t => t.Id == id);

            if (rowsDeleted == 0)
            {
                return NotFound(null);
            }
            else
            {
                return Ok();
            }
        }
    }
}