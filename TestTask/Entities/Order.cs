using System;
using System.Collections.Generic;

namespace TestTask.Entities
{
    public partial class Order
    {
        public long Id { get; set; }
        public double Cost { get; set; }
        public DateTime OrderTime { get; set; }
        public int StatusId { get; set; }
        public long ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual OrderStatus Status { get; set; } = null!;
    }
}
