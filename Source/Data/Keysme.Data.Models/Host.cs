namespace Keysme.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Spatial;

    public class Host
    {
        private ICollection<Image> images;

        public Host()
        {
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

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

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public bool IsInstantBook { get; set; }

        [Required]
        [MaxLength(100)]
        public string HostName { get; set; }

        public bool IsApproved { get; set; }

        public ListedType ListedType { get; set; }
        
        [MaxLength(1000)]
        public string UnlistedReason { get; set; }

        public DateTime? SnoozedFrom { get; set; }

        public DateTime? SnoozedTo { get; set; }

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
        
        public DbGeography GeoPoint { get; set; }
        
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
        public string WirelessName { get; set; }
        
        [MaxLength(64)]
        public string WirelessPassword { get; set; }
        
        [MaxLength(1000)]
        public string HouseManual { get; set; }

        [Required]
        [MaxLength(20)]
        public string MainPhone { get; set; }

        [Required]
        [MaxLength(20)]
        public string ReservationPhone { get; set; }

        public ICollection<Image> Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
            }
        }
    }

    public enum ListedType
    {
        Unlisted = 0,
        Snoozed = 1,
        Listed = 2
    }
}
