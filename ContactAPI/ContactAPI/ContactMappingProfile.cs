using AutoMapper;
using ContactAPI.Entities;
using ContactAPI.Models;

namespace ContactAPI
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactDto>();

            CreateMap<CreateContactDto, Contact>();

            CreateMap<UpdateContactDto, Contact>();
        }
    }
}
