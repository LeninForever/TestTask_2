using System;
using System.Collections.Generic;

namespace TestTask.Entities
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
