using TestTask.Dto.Statistics;
using TestTask.Entities;

namespace TestTask.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<IEnumerable<AvgCostByHoursDto>> GetAvgCostByHoursAsync();
        public Task<IEnumerable<ClientsOrdersSumCostInBirthdayDto>> GetClientsOrdersSumCostInBirthdayAsync();
    }
}
