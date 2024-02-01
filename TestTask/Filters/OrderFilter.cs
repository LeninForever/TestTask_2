using TestTask.Entities;

namespace TestTask.Filters
{
    public class OrderFilter : PageFilterBase
    {
        /// <summary>
        /// Стоимость товара от
        /// </summary>
        public double? CostFrom { get; set; }

        /// <summary>
        /// Стоимость товара до
        /// </summary>
        public double? CostTo { get; set; }

        /// <summary>
        /// Время покупки товара от
        /// </summary>
        public DateTime? OrderTimeFrom { get; set; }

        /// <summary>
        /// Время покупки товара до
        /// </summary>
        public DateTime? OrderTimeTo { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatuses? Status { get; set; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public int? ClientId { get; set; }
    }
}
