using LinqKit;
using Microsoft.EntityFrameworkCore;
using TestTask.Context;
using TestTask.Entities;
using TestTask.Repositories.Filters;
using TestTask.Repositories.Interfaces;

namespace TestTask.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly int _defaultLimit;

        public OrderRepository(OrderDbContext orderDbContext, int defaultLimit)
        {
            _orderDbContext = orderDbContext;
            _defaultLimit = defaultLimit;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            await _orderDbContext.Orders.AddAsync(entity);
            
            return entity;
        }

        public async Task DeleteAsync(Order entity)
        {
            _orderDbContext.Orders.Remove(entity);
        }

        public async Task<Order?> FindAsync(long id)
        {
            return await _orderDbContext.Orders.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(OrderEntityFilter filter)
        {
            var predicateBuilder = PredicateBuilder.New<Order>();

            predicateBuilder.DefaultExpression = p => true;

            if (filter.ClientId.HasValue)
                predicateBuilder.And(o => o.ClientId == filter.ClientId.Value);

            if (filter.Status.HasValue)
                predicateBuilder.And(o => o.StatusId == (int)filter.Status.Value);

            if (filter.CostFrom.HasValue)
                predicateBuilder.And(o => o.Cost >= filter.CostFrom);

            if (filter.CostTo.HasValue)
                predicateBuilder.And(o => o.Cost <= filter.CostTo);

            if (filter.OrderTimeFrom.HasValue)
                predicateBuilder.And(o => o.OrderTime >= filter.OrderTimeFrom);

            if (filter.OrderTimeTo.HasValue)
                predicateBuilder.And(o => o.OrderTime <= filter.OrderTimeTo);

            return await _orderDbContext.Orders
                .Include(o => o.Status)
                .Where(predicateBuilder)
                .Skip(filter.Offset ?? 0)
                .Take(filter.Limit ?? _defaultLimit)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _orderDbContext.SaveChangesAsync();
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _orderDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
