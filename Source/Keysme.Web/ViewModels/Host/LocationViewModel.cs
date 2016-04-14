namespace Keysme.Web.ViewModels.Host
{
    using System.ComponentModel.DataAnnotations;

    using Automapper;

    using Data.Models;

    public class LocationViewModel : IMapFrom<Host>, IMapTo<Host>
    {
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        
        [MaxLength(255)]
        [Display(Name = "Apt, Suite, Bldg. (optional)")]
        public string Apartment { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }
    }
}