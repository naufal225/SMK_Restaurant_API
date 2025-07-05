using AutoMapper;
using SMK_Restaurant_API.Dto;
using SMK_Restaurant_API.Models;

namespace SMK_Restaurant_API.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Msmenu, MenuDto>()
                .ForMember(dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => $"images/{src.PhotoUniqueName}"));
        }

    }
}
