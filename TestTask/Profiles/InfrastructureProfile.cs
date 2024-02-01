using AutoMapper;
using TestTask.Filters;
using TestTask.Repositories.Filters;

namespace TestTask.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Filters.PageFilterBase, Repositories.Filters.PageFilterBase>()
                .ForMember(f => f.Limit, f => f.MapFrom(x => x.PageSize))
                .ForMember(f => f.Offset, f => f.MapFrom(x => (x.PageNumber - 1) * x.PageSize));

            CreateMap<Repositories.Filters.PageFilterBase, Filters.PageFilterBase>()
                .ForMember(f => f.PageSize, f => f.MapFrom(x => x.Limit))
                .ForMember(f => f.PageNumber, f => f.MapFrom(x => x.Offset / x.Limit - 1));

        }
    }
}
