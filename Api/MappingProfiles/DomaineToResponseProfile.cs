using Api.Contracts.V1.Responses;
using Api.Domain;
using AutoMapper;
using System.Linq;

namespace Api.MappingProfiles
{
    public class DomaineToResponseProfile : Profile
    {
        public DomaineToResponseProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Tags,opt => opt.MapFrom(src => src.Tags.Select(x => x.TagName)));
        }
    }
}
