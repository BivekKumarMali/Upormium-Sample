using System.ComponentModel.DataAnnotations;

namespace Upormium.DomainModel.ApplicationClasses.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is mandatory.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is mandatory.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int LockoutOnFailure { get; set; }
    }
}
