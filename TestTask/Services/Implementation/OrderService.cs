using AutoMapper;
using TestTask.Dto.Order;
using TestTask.Entities;
using TestTask.Filters;
using TestTask.Repositories.Filters;
using TestTask.Repositories.Implementation;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            Order order = _mapper.Map<Order>(dto);

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> DeleteAsync(long id)
        {
            Order? order = await _orderRepository.FindAsync(id);

            if (order == null)
                return null;

            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();
            return _mapper.Map<OrderDto?>(order);

        }

        public async  Task<IEnumerable<OrderWithStatusDto>> GetAllOrdersAsync(OrderFilter filter)
        {
            OrderEntityFilter entityFilter = _mapper.Map<OrderEntityFilter>(filter);

            IEnumerable<Order>? orders = await _orderRepository.GetAllAsync(entityFilter);

            if (orders == null)
                return Enumerable.Empty<OrderWithStatusDto>();

            return orders.Select(x => _mapper.Map<OrderWithStatusDto>(x));
        }

        public async Task<OrderWithStatusDto?> GetOrderByIdAsync(long id)
        {

            Order? order = await _orderRepository.FindAsync(id);

            if (order == null)
                return null;

            return _mapper.Map<OrderWithStatusDto>(order);
        }

        public async Task<OrderDto?> UpdateAsync(OrderDto dto)
        {
            Order? order = await _orderRepository.FindAsync(dto.Id);

            if (order == null)
                return null;

            order.StatusId = (int)dto.Status;
            order.Cost = dto.Cost;
            order.OrderTime= dto.OrderTime;
            order.ClientId = dto.ClientId;

            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderWithStatusDto>(order);
        }
    }
}
