using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class ProductShopping : Product
    {
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor
        {
            get
            {
                decimal a = ((8 * Precio) / 10) * Count;    // 80% Up to 100
                decimal b = ((9 * Precio) / 10) * Count;    // 90%
                return (Count > 100) ? a : b;
            }
        }


        [Required(ErrorMessage = "The field {0} is Requierd")]
        [Display(Name = "Quantity")]
        public int Count { get; set; }
    }
}