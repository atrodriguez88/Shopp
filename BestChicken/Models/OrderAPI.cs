using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class OrderAPI
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderState OrderState { get; set; }
        public Costumer Costumer { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}