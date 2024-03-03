using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchi.Domain.Entities
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "番号")]
        public int Villa_Number { get; set; }

        [ForeignKey("Villa")]
        [Display(Name = "ID")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa Villa { get; set; }
        [Display(Name = "備考")]
        public string? SpecialDetails { get; set; }
    }
}
