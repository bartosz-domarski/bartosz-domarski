using AutoMapper;
using FiberFresh.Application.Dtos;
using FiberFresh.Domain.Entities;

namespace FiberFresh.Application.Mappings
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => new Client
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Email = src.Email,
                    PhoneNumber = src.PhoneNumber,
                    City = src.City,
                    Street = src.Street,
                    Floor = src.Floor,
                    IsElevator = src.IsElevator
                }));

            CreateMap<ServiceDto, Service>();
        }
    }
}
