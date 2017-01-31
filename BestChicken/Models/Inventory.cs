using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        [Display(Name = "Product")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {2} to {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "The field {0} is Requierd")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is Requierd")]
        [Display(Name = "Product Date")]
        public DateTime InventoryDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is Requierd")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}