using System.ComponentModel.DataAnnotations;

namespace Litgraph.IdentityServer.Model.Authorization
{
    public class SignInModel
    {
        [Required, Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, StringLength(50, MinimumLength = 6, ErrorMessage = "Password has to be at least 6 characters long")]
        public string Password { get; set; }
        public bool RememberLogin { get; set; } = true;
        public string ReturnUrl { get; set; }
    }

    public class SignUpModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, StringLength(50, MinimumLength = 6, ErrorMessage = "Password has to be at least 6 characters long")]
        public string Password { get; set; }

        [Required, Compare(nameof(SignUpModel.Password), ErrorMessage = "Passwords do not match"), Display(Name = "Password verification")]
        public string PasswordRepeat { get; set; }

        public string ReturnUrl { get; set; }
    }
}