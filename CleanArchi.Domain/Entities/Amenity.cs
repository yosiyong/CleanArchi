using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CleanArchi.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public required string Name { get; set; }
        [Display(Description = "備考")]
        public string? Description { get; set; }

        [ForeignKey("Villa")]
        [Display(Name = "ID")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa Villa { get; set; }

    }
}
