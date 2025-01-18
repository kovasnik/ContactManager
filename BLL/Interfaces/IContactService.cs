using ContactManager.DTO;
using ContactManager.Models;
using System.Threading.Tasks;

namespace ContactManager.BLL.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task ProcessCsvFileAsync(IFormFile csvFile);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(ContactDTO contactDTO);
    }
}
