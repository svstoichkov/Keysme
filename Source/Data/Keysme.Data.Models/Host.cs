namespace Keysme.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Host
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? AmenitiesId { get; set; }

        public Amenities Amenities { get; set; }

        public HostType Type { get; set; }

        public int MaxGuests { get; set; }

        public int RoomsCount { get; set; }

        public int BedsCount { get; set; }

        public int BathsCount { get; set; }

        public RoomType RoomType { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(40)]
        public string Header { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        //TODO: currency

        public bool IsInstantBook { get; set; }

        [Required]
        [MaxLength(100)]
        public string HostName { get; set; }

        public bool IsApproved { get; set; }

        public bool? IsListed { get; set; }
        
        [MaxLength(1000)]
        public string UnlistedReason { get; set; }

        public DateTime SnoozedFrom { get; set; }

        public DateTime SnoozedTo { get; set; }

        public bool SmokingAllowed { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCode { get; set; }

        //TODO: fix types and attributes if necessary

        [Required]
        [MaxLength(100)]
        public string Latitude { get; set; }

        [Required]
        [MaxLength(100)]
        public string Longitude { get; set; }

        [Required]
        [MaxLength(100)]
        public string GeoPoint { get; set; }

        [Required]
        [MaxLength(100)]
        public string LocationName { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; }

        //TODO: types???
        public int CheckInAfter { get; set; }

        public int CheckOutBefore { get; set; }

        public CancellationPolicy CancellationPolicy { get; set; }

        [Required]
        [MaxLength(32)]
        public string WirelessName { get; set; }

        [Required]
        [MaxLength(64)]
        public string WirelessPassword { get; set; }

        [Required]
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
