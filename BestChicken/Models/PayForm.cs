using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class PayForm
    {
        [Key]
        public int PayFormId { get; set; }

        [Display(Name = "Type of Pay")]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<Costumer> Costumers { get; set; }

    }
}