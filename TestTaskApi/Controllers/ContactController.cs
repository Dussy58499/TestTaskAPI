using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models.DataTransferObjects;
using Service.Interfaces;
using System.Threading.Tasks;
using System;

namespace TestTaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactDTO contactDTO)
        {
            try
            {
                await _contactService.CreateContact(contactDTO);
                return Ok("Contact created successfully");

            }

            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}