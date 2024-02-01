using TestTask.Entities;

namespace TestTask.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        public Task<IEnumerable<AvgCostByHours>> GetAvgCostByHoursAsync();
        public Task<IEnumerable<ClientsOrdersSumCostInBirthday>> GetClientsOrdersSumCostInBirthdayAsync();
    }
}
