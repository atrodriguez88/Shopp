using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(30, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public string ProductName { get; set; }

        [StringLength(30, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public string Company { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public decimal Precio { get; set; }

        [StringLength(50, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Product")]
        public int InventoryId { get; set; }

        [JsonIgnore]
        public virtual Inventory Inventory { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public ICollection<PoductSupplier> PoductSuppliers { get; set; }
        [JsonIgnore]
        public ICollection<ShoppingDetail> ShoppingDetails { get; set; }
    }
}