using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class Costumer
    {
        [Key]
        public int CostumerId { get; set; }
        [Display(Name = "First Name")]
        [StringLength(30,ErrorMessage = "The camp {0} must be between {2} to {1}",MinimumLength = 3)]
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

        [NotMapped]
        public string FullName { get
        {
            return FirstName + " " + LastName;
        } }
        public int PayFormId { get; set; }
        [JsonIgnore]
        public virtual PayForm PayForm { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}