using YetAnotherContactApp.DTOs;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApp.Interfaces
{
    public interface IContactRepository
    {
        Task AddContact(ContactDto newContact);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task UpdateContact(ContactUpdateDto contactUpdate);
        Task DeleteContact(int id);
    }
}
