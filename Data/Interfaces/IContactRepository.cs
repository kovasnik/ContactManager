using ContactManager.Models;

namespace ContactManager.Data.Interfaces
{
    public interface IContactRepository
    {
        Task AddRangeAsync(IEnumerable<Contact> contacts);
        Task DeleteAsync(Contact contact);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task UpdateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id);

    }
}
