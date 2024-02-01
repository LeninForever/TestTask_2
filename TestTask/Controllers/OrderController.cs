using Microsoft.AspNetCore.Mvc;
using TestTask.Dto.Order;
using TestTask.Filters;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery] OrderFilter filter)
        {
            try
            {
                IEnumerable<OrderDto> orders = await _orderService.GetAllOrdersAsync(filter);

                return Ok(orders);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(new { message = "Формат даты должен соответствовать форму yyyy-MM-dd" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При получении инфорации о заказах произошла ошибка" });
            }
        }
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetOrderByIdAsync(long id)
        {
            try
            {
                OrderDto? orderDto = await _orderService.GetOrderByIdAsync(id);
                if (orderDto == null)
                    return NotFound(new { message = $"Заказ с id:{id} не найден" });

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При поиске заказа произошла ошибка" });

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto orderDto)
        {
            try
            {
                OrderDto? newOrderDto = await _orderService.CreateAsync(orderDto);

                return Ok(newOrderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При добавлении заказа произошла ошибка" });

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderDto orderDto)
        {
            try
            {
                OrderDto? updatedOrderDto = await _orderService.UpdateAsync(orderDto);
                if (updatedOrderDto == null)
                    return NotFound(new { message = $"Заказ с id:{orderDto.Id} не найден" });

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);

                return StatusCode(500, new { message = "При обновлении информации о заказе произошла ошибка" });
            }
        }
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteOrderAsync(long id)
        {
            try
            {
                OrderDto? order = await _orderService.DeleteAsync(id);
                if (order == null)
                    return NotFound(new { message = $"Заказ с id:{id} не найден" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, new { message = "При удалении заказа произошла ошибка" });

            }
        }
    }
}
