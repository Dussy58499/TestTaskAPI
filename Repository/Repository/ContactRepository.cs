using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interfaces;
using Repository.Models.Domain;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<Contact> GetContactByEmailAsync(string email)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
