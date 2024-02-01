using Microsoft.AspNetCore.Mvc;
using TestTask.Dto.Statistics;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        [HttpGet("statistics/avg-cost-by-hours")]
        public async Task<IActionResult> GetAvgCostByHoursAsync()
        {
            try
            {
                IEnumerable<AvgCostByHoursDto> avgCostByHoursDtos = await _statisticsService
                    .GetAvgCostByHoursAsync();

                return Ok(avgCostByHoursDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При получении среднего чека по часам произошла ошибка" });
            }
        }
        [HttpGet("statistics/clients-orders-sum-cost-birthday")]
        public async Task<IActionResult> GetClientsOrdersSumCostInBirthdayAsync()
        {
            try
            {
                IEnumerable<ClientsOrdersSumCostInBirthdayDto> clientsOrdersSumCostInBirthdayDtos =
                    await _statisticsService.GetClientsOrdersSumCostInBirthdayAsync();

                return Ok(clientsOrdersSumCostInBirthdayDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При получении суммы заказов по каждому клиенту в его ДР произошла ошибка" });
            }
        }
    }
}


