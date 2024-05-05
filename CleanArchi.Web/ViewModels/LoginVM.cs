using System.ComponentModel.DataAnnotations;

namespace CleanArchi.Web.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "メールアドレスは必須入力です。")]
        public string Email { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードは必須入力です。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? RedirectUrl { get; set; }
    }
}
