using AutoMapper;
using TestTask.Dto.Statistics;
using TestTask.Entities;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementation
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;
        public StatisticsService(IStatisticsRepository statisticsRepository, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AvgCostByHoursDto>> GetAvgCostByHoursAsync()
        {
            IEnumerable<AvgCostByHours> avgCostByHoursItems = await _statisticsRepository.GetAvgCostByHoursAsync();
            
            return avgCostByHoursItems.Select(x => _mapper.Map<AvgCostByHoursDto>(x));
        }

        public async Task<IEnumerable<ClientsOrdersSumCostInBirthdayDto>> GetClientsOrdersSumCostInBirthdayAsync()
        {
            IEnumerable<ClientsOrdersSumCostInBirthday> clientsOrdersSumInBirthdayItems = await _statisticsRepository
                .GetClientsOrdersSumCostInBirthdayAsync();

            return clientsOrdersSumInBirthdayItems.Select(x => _mapper.Map<ClientsOrdersSumCostInBirthdayDto>(x));
        }
    }
}
