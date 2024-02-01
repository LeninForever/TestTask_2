using TestTask.Dto.Client;

namespace TestTask.Dto.Order
{
    public record OrderWithClientInfoDto : OrderDto
    {
        public ClientDto Client { get; init; } = null!;
    }
}
