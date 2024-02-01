using AutoMapper;
using TestTask.Dto.Order;
using TestTask.Entities;
using TestTask.Filters;
using TestTask.Repositories.Filters;

namespace TestTask.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(x => x.StatusId, x => x.MapFrom(x => (int)x.Status))
                .ForMember(x => x.Status, x => x.Ignore())
                ;

            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Status, x => x.MapFrom(x => (OrderStatuses)x.StatusId))
                ;

            CreateMap<Order, OrderWithStatusDto>()
                 .IncludeBase<Order, OrderDto>()
                 .ForMember(x => x.OrderStatus, x => x.MapFrom(x => new OrderStatusDto(x.StatusId, x.Status.Title)))
                 .ReverseMap();

            CreateMap<OrderFilter, OrderEntityFilter>()
                .IncludeBase<Filters.PageFilterBase, Repositories.Filters.PageFilterBase>()
                /*.ForMember(x => x.OrderTimeFrom, x => x.MapFrom(x => string.IsNullOrEmpty(x.OrderTimeFrom) ? null : (DateTime?)DateTime.ParseExact(x.OrderTimeFrom, "yyyy-MM-dd")))
                .ForMember(x => x.BirthDateTo, x => x.MapFrom(x => string.IsNullOrEmpty(x.BirthDateTo) ? null : (DateOnly?)DateOnly.ParseExact(x.BirthDateTo, "yyyy-MM-dd")))*/
                ;
        }
    }
}
