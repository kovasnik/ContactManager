using ContactManager.Data.Interfaces;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
               _context = context;
        }
        public async Task AddRangeAsync(IEnumerable<Contact> contacts)
        {
            await _context.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            _context.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }
        public async Task UpdateAsync(Contact contact)
        {
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
