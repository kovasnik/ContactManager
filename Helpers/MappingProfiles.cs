using AutoMapper;
using ContactManager.DTO;
using ContactManager.Models;

namespace ContactManager.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ContactDTO, Contact>();
            CreateMap<Contact, ContactDTO>();
        }
    }
}
