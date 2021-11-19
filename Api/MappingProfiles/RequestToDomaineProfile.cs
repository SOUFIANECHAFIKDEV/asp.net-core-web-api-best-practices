using Api.Contracts.V1.Requests.Queries;
using Api.Domain;
using AutoMapper;

namespace Api.MappingProfiles
{
    public class RequestToDomaineProfile : Profile
    {
        public RequestToDomaineProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
