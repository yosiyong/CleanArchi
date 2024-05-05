using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchi.Web.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "メールアドレスは必須入力です。")]
        public string Email { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードは必須入力です。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "パスワード再入力")]
        [Required(ErrorMessage = "パスワード再入力は必須入力です。")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "氏名")]
        [Required(ErrorMessage = "氏名は必須入力です。")]
        public string Name { get; set; }

        [Display(Name="電話番号")]
        public string? PhoneNumber { get; set; }

        public string? RedirectUrl { get; set; }
        public string? Role {  get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
