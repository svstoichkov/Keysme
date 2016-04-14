namespace Keysme.Web.ViewModels.Host
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Automapper;

    using Data.Models;

    public class HostMainInformationViewModel : IMapTo<Host>, IMapFrom<Host>
    {
        [MaxLength(100)]
        [Display(Name = "Hotel/Motel/B&B name (optional)")]
        public string HostName { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Host type")]
        public HostType Type { get; set; }

        [Required]
        [Display(Name = "Room type")]
        public RoomType RoomType { get; set; }

        [Required]
        [Display(Name = "Rooms count")]
        public int? RoomsCount { get; set; }

        [Required]
        [Display(Name = "Guests per room")]
        public int? MaxGuests { get; set; }

        [Required]
        [Display(Name = "Beds per room")]
        public int? BedsCount { get; set; }

        [Required]
        [Display(Name = "Baths per room")]
        public int? BathsCount { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public SelectList Currencies { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int? CurrencyId { get; set; }

        public bool IsInstantBook { get; set; }

        [Required]
        [Display(Name = "Cancellation policy")]
        public CancellationPolicy CancellationPolicy { get; set; }

        [Required]
        [Display(Name = "Check in after")]
        public int? CheckInAfter { get; set; }

        [Required]
        [Display(Name = "Check out before")]
        public int? CheckOutBefore { get; set; }

        [MaxLength(32)]
        [Display(Name = "WiFi name")]
        public string WiFiName { get; set; }

        [MaxLength(64)]
        [Display(Name = "WiFi password")]
        public string WiFiPassword { get; set; }

        [MaxLength(1000)]
        [Display(Name = "House manual")]
        public string HouseManual { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Main phone")]
        public string MainPhone { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Reservation phone")]
        public string ReservationPhone { get; set; }

        public bool SmokingAllowed { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string Country { get; set; }
        //
        //[Required]
        //[MaxLength(100)]
        //public string City { get; set; }
        //
        //[Required]
        //[MaxLength(255)]
        //public string Address { get; set; }
        //
        //[Required]
        //[MaxLength(50)]
        //public string State { get; set; }
        //
        //[Required]
        //[MaxLength(20)]
        //public string PostalCode { get; set; }

        //[Required]
        //[MaxLength(3)]
        //public string CountryCode { get; set; }

        //public decimal Latitude { get; set; }

        //public decimal Longitude { get; set; }

        //public DbGeography GeoPoint { get; set; }

        //[MaxLength(100)]
        //public string LocationName { get; set; }

        //[Required]
        //[MaxLength(1000)]
        //public string Comment { get; set; }
    }
}