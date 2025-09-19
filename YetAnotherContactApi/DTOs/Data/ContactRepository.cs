using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YetAnotherContactApp.DTOs;
using YetAnotherContactApp.Interfaces;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApi.DTOs.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddContact(ContactDto newContact)
        {
            Contact contact = _mapper.Map<Contact>(newContact);

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContact(int id)
        {
            Contact? contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            //using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress))
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        IEnumerable<Contact> contacts = await response.Content.ReadFromJsonAsync<IEnumerable<Contact>>();
            //        return contacts;
            //    }
            //    else
            //    {
            //        throw new Exception(response.ReasonPhrase);
            //    }
            //}

            return await _context.Contacts
               .ToListAsync();
        }

        public async Task UpdateContact(ContactUpdateDto contactUpdate)
        {
            Contact? contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactUpdate.Id);

            if (contact != null)
            {
                _mapper.Map(contactUpdate, contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
