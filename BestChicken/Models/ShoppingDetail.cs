using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BestChicken.Models
{
    public class ShoppingDetail
    {
        [Key]
        public int ShoppingDetailId { get; set; }

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

        public int ShoppingId { get; set; }
        [JsonIgnore]
        public virtual Shopping Shopping { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}