using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}