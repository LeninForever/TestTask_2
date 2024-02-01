using Microsoft.AspNetCore.Mvc;
using TestTask.Dto.Client;
using TestTask.Filters;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClientsAsync([FromQuery] ClientFilter filter)
        {
            try
            {
                IEnumerable<ClientDto> clients = await _clientService.GetAllClientsAsync(filter);

                return Ok(clients);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(new { message = "Формат даты должен соответствовать форму yyyy-MM-dd" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При получении инфорации о клиентах произошла ошибка" });
            }
        }
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetClientByIdAsync(long id)
        {
            try
            {
                ClientDto? clientDto = await _clientService.GetClientByIdAsync(id);
                if (clientDto == null)
                    return NotFound(new { message = $"Клиент с id:{id} не найден" });

                return Ok(clientDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При поиске клиента произошла ошибка" });

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateClientAsync([FromBody] ClientDto clientDto)
        {
            try
            {
                ClientDto? newClientDto = await _clientService.CreateAsync(clientDto);

                return Ok(newClientDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При добавлении клиента произошла ошибка" });

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientAsync([FromBody] ClientDto clientDto)
        {
            try
            {
                ClientDto? updatedClientDto = await _clientService.UpdateAsync(clientDto);
                if (updatedClientDto == null)
                    return NotFound(new { message = $"Клиент с id:{clientDto.Id} не найден" });

                return Ok(clientDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При обновлении информации о клиенте произошла ошибка" });
            }
        }
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteClientAsync(long id)
        {
            try
            {
                ClientDto? client = await _clientService.DeleteAsync(id);
                if (client == null)
                    return NotFound(new { message = $"Клиент с id:{id} не найден" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При удалении клиента произошла ошибка" });

            }
        }

    }
}
