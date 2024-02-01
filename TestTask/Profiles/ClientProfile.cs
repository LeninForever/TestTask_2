using AutoMapper;
using System.Globalization;
using TestTask.Dto.Client;
using TestTask.Entities;
using TestTask.Filters;
using TestTask.Repositories.Filters;

namespace TestTask.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDto, Client>()
                .ReverseMap();


            CreateMap<ClientFilter, ClientEntityFilter>()
                .IncludeBase<Filters.PageFilterBase, Repositories.Filters.PageFilterBase>()
                .ForMember(x => x.BirthDateFrom, x => x.MapFrom(x => string.IsNullOrEmpty(x.BirthDateFrom) ? null : (DateOnly?)DateOnly.ParseExact(x.BirthDateFrom, "yyyy-MM-dd")))
                .ForMember(x => x.BirthDateTo, x => x.MapFrom(x => string.IsNullOrEmpty(x.BirthDateTo) ? null : (DateOnly?)DateOnly.ParseExact(x.BirthDateTo, "yyyy-MM-dd")))
                ;
        }
    }
}
