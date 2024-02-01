using TestTask.Entities;

namespace TestTask.Dto.Order
{
    public record OrderDto
    {
        public long Id { get; init; }
        public double Cost { get; init; }
        public DateTime OrderTime { get; init; }
        public OrderStatuses Status { get; init; }
        public long ClientId { get; init; }
    }
}
