using AutoMapper;
using SMK_Restaurant_API.Dto;
using SMK_Restaurant_API.Models;

namespace SMK_Restaurant_API.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(
                    dest => dest.ReviewerName,
                    opt => opt.MapFrom(src => src.Member!.Name
                )
            );
        }
    }
}
