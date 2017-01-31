using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class BestChickenContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BestChickenContext() : base("name=BestChickenContext")
        {
        }
        /* Evitar Borrada en Cascada */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<BestChicken.Models.Costumer> Costumers { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.PayForm> PayForms { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<BestChicken.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<BestChicken.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.Inventory> Inventories { get; set; }

        public System.Data.Entity.DbSet<BestChicken.Models.Supplier> Suppliers { get; set; }
        public System.Data.Entity.DbSet<BestChicken.Models.Shopping> Shoppings { get; set; }
        public System.Data.Entity.DbSet<BestChicken.Models.ShoppingDetail> ShoppingDetails { get; set; }
    }
}
