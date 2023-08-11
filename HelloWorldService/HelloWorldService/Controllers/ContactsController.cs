using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects,
// visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private static int currentId = 101;

        private static List<Contact> contacts = new List<Contact>();

        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
            _logger.LogInformation("INFO");
            _logger.LogWarning("WARNING");
            _logger.LogError("ERROR");
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult Get()
        {
            int x = 1;
            x = x / (x - 1);
            return Ok(contacts);
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
        public IActionResult Post([FromBody] Contact value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                var errorResponse = new ErrorResponse
                {
                    DBCode = ErrorType.MissingField,
                    Message = "Null or Empty Name",
                    FieldName = "Name",
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
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound(null);
            }

            // DO NOT copy the contact.ID field from value

            contact.Name = value.Name;
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