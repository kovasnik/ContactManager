using ContactManager.BLL.Interfaces;
using ContactManager.DTO;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contract = await _contactService.GetAllContactsAsync();
            return Ok(contract);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
        {
            try
            {
                await _contactService.ProcessCsvFileAsync(file);
                return Ok(new { Message = "File processed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var isDeleted = await _contactService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { Message = "Contact not found" });
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromBody] ContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var isUpdated = await _contactService.UpdateAsync(contactDTO);

            if(!isUpdated)
            {
                return NotFound(new { Message = "Contact not found" });
            }

            return Ok(new { Message = "Contact updated successfully" });
        }
    }
}
