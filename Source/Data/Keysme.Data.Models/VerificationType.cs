namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum VerificationType
    {
        [Display(Name = "Driver License")]
        DriverLicense = 0,
        Passport = 1,
        Government = 2
    }
}