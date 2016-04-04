namespace Keysme.Web.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Global;

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "First name")]
        [MaxLength(ValidationConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [MaxLength(ValidationConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}