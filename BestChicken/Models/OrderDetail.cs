using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is Requierd")]
        public decimal Valor { get; set; }

        [StringLength(50, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is Requierd")]
        public int Count { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}