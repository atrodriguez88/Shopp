using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Shopping
    {
        [Key]
        public int ShoppingId { get; set; }

        [Required(ErrorMessage = "The field {0} is Requierd")]
        [Display(Name = "Date of Shopping")]
        public DateTime ShoppingDate { get; set; }

        public ShoppingState ShoppingState { get; set; }

        public int SupplierId { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        [JsonIgnore]
        public virtual ICollection<ShoppingDetail> ShoppingDetails { get; set; }
    }
}