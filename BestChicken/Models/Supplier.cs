using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        [StringLength(30,ErrorMessage = "The field {0} must be betwen {2} to {1}",MinimumLength = 2)]
        [Required(ErrorMessage = "The field {0} is Required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(30, ErrorMessage = "The field {0} must be betwen {2} to {1}", MinimumLength = 5)]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<PoductSupplier> PoductSuppliers { get; set; }

        [JsonIgnore]
        public ICollection<Shopping> Shoppings { get; set; }
    }
}