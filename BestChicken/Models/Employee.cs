using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestChicken.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public string LastName { get; set; }

        [Display(Name = "Birth of Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public DateTime BirthOfDate { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public string Email { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The camp {0} is Requierd")]
        public decimal Salary { get; set; }

        [StringLength(50, ErrorMessage = "The camp {0} must be between {2} to {1}", MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}