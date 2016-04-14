namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum VerificationType
    {
        [Display(Name = "Driver License")]
        DriverLicense = 1,
        Passport = 2,
        Government = 3
    }
}