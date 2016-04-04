namespace Keysme.Web.Models.Host
{
    using System.ComponentModel.DataAnnotations;

    using Automapper;

    using Data.Models;

    public class HostViewModel : IMapTo<Host>
    {
        public HostType Type { get; set; }

        public RoomType RoomType { get; set; }

        public int MaxGuests { get; set; }

        public int RoomsCount { get; set; }

        public int BedsCount { get; set; }

        public int BathsCount { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(40)]
        public string Header { get; set; }

        public decimal Price { get; set; }

        public int CurrencyId { get; set; }

        public bool IsInstantBook { get; set; }

        [Required]
        [MaxLength(100)]
        public string HostName { get; set; }

        public bool SmokingAllowed { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        //public DbGeography GeoPoint { get; set; }

        [MaxLength(100)]
        public string LocationName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; }

        //TODO: types???
        public int CheckInAfter { get; set; }

        public int CheckOutBefore { get; set; }

        public CancellationPolicy CancellationPolicy { get; set; }

        [MaxLength(32)]
        public string WiFiName { get; set; }

        [MaxLength(64)]
        public string WiFiPassword { get; set; }

        [MaxLength(1000)]
        public string HouseManual { get; set; }

        [Required]
        [MaxLength(20)]
        public string MainPhone { get; set; }

        [Required]
        [MaxLength(20)]
        public string ReservationPhone { get; set; }
    }
}