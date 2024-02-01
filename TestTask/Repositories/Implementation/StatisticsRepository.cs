using Microsoft.EntityFrameworkCore;
using TestTask.Context;
using TestTask.Entities;
using TestTask.Repositories.Interfaces;

namespace TestTask.Repositories.Implementation
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly OrderDbContext _orderDbContext;

        public StatisticsRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<IEnumerable<AvgCostByHours>> GetAvgCostByHoursAsync()
        {
            return await _orderDbContext.AvgCostByHours
                .FromSqlRaw<AvgCostByHours>("select * from \"order\".get_avg_cost_by_hours()")
                .ToListAsync();
        }

        public async Task<IEnumerable<ClientsOrdersSumCostInBirthday>> GetClientsOrdersSumCostInBirthdayAsync()
        {
            return await _orderDbContext.ClientsOrdersSumCostInBirthdayDbSet
                .FromSqlRaw<ClientsOrdersSumCostInBirthday>("select * from \"order\".get_clients_orders_sum_cost_in_birthday()")
                .ToListAsync();
        }
    }
}
