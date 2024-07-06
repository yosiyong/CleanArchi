using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchi.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        
        [Display(Name = "物件名")]
        [MaxLength(50)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        public required string Name { get; set; }
        
        public string? Description { get; set; }
        
        [Display(Name = "一泊価格")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        [Range(1, 10000000, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        public double Price { get; set; }
        
        [Display(Name = "面積")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        [Range(10, 1000, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        public int Sqft { get; set; }
        
        [Display(Name = "使用率")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        [Range(0, 100, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resource.ValidationMessage))]
        public int Occupancy { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        
        [Display(Name = "イメージUrl")]
        public string? ImageUrl { get; set; }
        
        public DateTime? Created_Date { get; set; }
        
        public DateTime? Updated_Date { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> VillaAmenity { get; set; }

        [NotMapped]
        public bool IsAvailable { get; set; }
    }

}
