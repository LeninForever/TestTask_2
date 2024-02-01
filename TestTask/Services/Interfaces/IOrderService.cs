using TestTask.Dto.Order;
using TestTask.Filters;

namespace TestTask.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderWithStatusDto>> GetAllOrdersAsync(OrderFilter filter);
        Task<OrderWithStatusDto?> GetOrderByIdAsync(long id);
        Task<OrderDto> CreateAsync(OrderDto dto);

        Task<OrderDto?> UpdateAsync(OrderDto dto);
        Task<OrderDto?> DeleteAsync(long id);
    }
}
