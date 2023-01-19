using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam2023.Models
{
    public class GiftCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a value for the gift card name")]
        public string Denumire { get; set; }

        [Required(ErrorMessage = "Please enter a value for the gift card description")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Please enter a value for the gift card date")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(GiftCard), "ValidateDate")]
        public DateTime DataExp { get; set; }

        [Required(ErrorMessage = "Please enter a value for the gift card procent")]
        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 100")]
        public int Procent { get; set; }

        [Required(ErrorMessage = "Please enter a value for the gift card brand")]
        public int? BrandId { get; set; }

        public Brand? Brand { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Brands { get; set; }

        public static ValidationResult ValidateDate(DateTime? date)
        {
            if (date == null)
            {
                return new ValidationResult("Please enter a date"); 
            }

            if (date < DateTime.Now)
            {
                return new ValidationResult("Please enter a date in the future");
            }

            return ValidationResult.Success;
        }
    }
}
