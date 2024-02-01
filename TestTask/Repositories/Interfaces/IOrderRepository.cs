using TestTask.Entities;
using TestTask.Repositories.Filters;
using TestTask.Repositories.Infrastructure;

namespace TestTask.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order, OrderEntityFilter>
    {
    }
}
