using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BestChicken.Models;

namespace BestChicken.ViewModels
{
    public class OrderView
    {
        public Costumer Costumer { get; set; }
        public List<ProductOrder> Products { get; set; }
        public Product Product { get; set; }
    }
}