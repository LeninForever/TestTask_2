using AutoMapper;
using TestTask.Dto.Client;
using TestTask.Dto.Statistics;
using TestTask.Entities;

namespace TestTask.Profiles
{
    public class StatisticsProfile : Profile
    {
        public StatisticsProfile()
        {
            CreateMap<AvgCostByHours, AvgCostByHoursDto>()
               .ReverseMap();

            CreateMap<ClientsOrdersSumCostInBirthday, ClientsOrdersSumCostInBirthdayDto>()
               .ReverseMap();

        }
    }
}
