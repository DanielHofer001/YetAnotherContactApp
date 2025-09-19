using Microsoft.AspNetCore.Mvc;
using YetAnotherContactApp.DTOs;
using YetAnotherContactApp.Interfaces;

namespace YetAnotherContactApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactRepository contactRepository) : ControllerBase
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        [HttpPost]
        public async Task<ActionResult> AddContact(ContactDto contact)
        {
            await _contactRepository.AddContact(contact);
            return Ok(contact);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAllContacts()
        {
            return Ok(await _contactRepository.GetAllContacts());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateContact(ContactUpdateDto contactUpdate)
        {
            await _contactRepository.UpdateContact(contactUpdate);
            return Ok(contactUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactRepository.DeleteContact(id);
            return Ok();
        }
    }
}
