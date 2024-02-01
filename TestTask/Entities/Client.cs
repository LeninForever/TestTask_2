using System;
using System.Collections.Generic;

namespace TestTask.Entities
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
