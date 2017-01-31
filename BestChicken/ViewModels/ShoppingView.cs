using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BestChicken.Models;

namespace BestChicken.ViewModels
{
    public class ShoppingView
    {
        public Supplier Supplier { get; set; }
        public List<ProductShopping> Products { get; set; }
        //public Product Product { get; set; }
        public ProductShopping ProductShopping { get; set; }
    }
}