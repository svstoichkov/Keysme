namespace Keysme.Web.ViewModels.Account
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
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Day of birth")]
        public int BirthDay { get; set; }

        [Required]
        [Display(Name = "Year of birth")]
        public int BirthYear { get; set; }

        [Required]
        [Display(Name = "Month of birth")]
        public int BirthMonth { get; set; }

        [Required]
        [Display(Name = "Terms and Conditions")]
        public bool? TermsAndConditions { get; set; }
    }
}