using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class PoductSupplier
    {
        [Key]
        public int PoductSupplierId { get; set; }

        public int ProductId { get; set; }

        public int SupplierId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
    }
}