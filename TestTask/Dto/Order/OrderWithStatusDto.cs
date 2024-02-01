namespace TestTask.Dto.Order
{
    public record OrderWithStatusDto : OrderDto
    {
        public OrderStatusDto OrderStatus { get; set; } = null!;
    }
}
