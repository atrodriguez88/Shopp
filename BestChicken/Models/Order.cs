using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The field {0} is Requierd")]
        [Display(Name = "Date of Order")]
        public DateTime OrderDate { get; set; }

        public OrderState OrderState { get; set; }

        public int CostumerId { get; set; }
        [JsonIgnore]
        public virtual Costumer Costumer  { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}