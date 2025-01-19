using AutoMapper;
using ContactManager.BLL.Interfaces;
using ContactManager.Data.Interfaces;
using ContactManager.DTO;
using ContactManager.Models;

namespace ContactManager.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task ProcessCsvFileAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                throw new ArgumentException("Invalid CSV file");
            }
            using var reader = new StreamReader(csvFile.OpenReadStream());
            var contacts = new List<Contact>(); 
            while(!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var fields = line?.Split(',');
                if (fields == null || fields.Length != 5)
                {
                    throw new ArgumentException("CSV file format is invalid");
                }

                try
                {
                    var contact = new Contact
                    {
                        Name = fields[0].Trim(),
                        DateOfBirth = DateOnly.Parse(fields[1].Trim()),
                        IsMarried = bool.Parse(fields[2].Trim()),
                        Phone = fields[3].Trim(),
                        Salary = decimal.Parse(fields[4].Trim())
                    };
                    contacts.Add(contact);
                }
                catch (Exception ex)
                {
                    throw new FormatException($"Error parsing CSV line: {line}. Details: {ex.Message}");
                }
            }
            await _contactRepository.AddRangeAsync(contacts);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await GetContactByIdAsync(id);
           
            if (contact == null)
            {
                return false;
            }

            await _contactRepository.DeleteAsync(contact);
            return true;
        }

        public async Task<bool> UpdateAsync(ContactDTO contactDTO)
        {
            var contact = await GetContactByIdAsync(contactDTO.Id);

            if (contact == null)
            {
                return false;
            }

            _mapper.Map(contactDTO, contact);
            await _contactRepository.UpdateAsync(contact);
            return true;
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }
    }
}
