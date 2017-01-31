using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class ProductOrder : Product
    {

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get
        {
            return Precio * Count;
        } }

        
        [Required(ErrorMessage = "The field {0} is Requierd")]
        [Display(Name = "Quantity")]
        public int Count { get; set; }

    }
}