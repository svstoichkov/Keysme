namespace Keysme.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Base;

    public class Host : BaseModel
    {
        private ICollection<Image> images;

        public Host()
        {
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public bool IsComplete { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Amenities Amenities { get; set; }
        
        public virtual ICollection<Image> Images
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

        //Main information
        [MaxLength(100)]
        public string HostName { get; set; }

        [MaxLength(40)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public HostType? Type { get; set; }

        public RoomType? RoomType { get; set; }

        public int? RoomsCount { get; set; }

        public int? MaxGuests { get; set; }

        public int? BedsCount { get; set; }

        public int? BathsCount { get; set; }

        public decimal? Price { get; set; }

        public int? CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public bool IsInstantBook { get; set; }

        public CancellationPolicy? CancellationPolicy { get; set; }

        public int CheckInAfter { get; set; }

        public int CheckOutBefore { get; set; }

        [MaxLength(32)]
        public string WiFiName { get; set; }

        [MaxLength(64)]
        public string WiFiPassword { get; set; }

        [MaxLength(1000)]
        public string HouseManual { get; set; }

        [MaxLength(20)]
        public string MainPhone { get; set; }

        [MaxLength(20)]
        public string ReservationPhone { get; set; }

        public bool SmokingAllowed { get; set; }
        
        //Location
        [MaxLength(50)]
        public string Country { get; set; }
        
        [MaxLength(100)]
        public string City { get; set; }
        
        [MaxLength(50)]
        public string State { get; set; }
        
        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Apartment { get; set; }
        
        [MaxLength(20)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        //Admin
        [MaxLength(1000)]
        public string Comment { get; set; }

        public bool IsApproved { get; set; }

        public ListedType? ListedType { get; set; }

        [MaxLength(1000)]
        public string UnlistedReason { get; set; }

        public DateTime? SnoozedFrom { get; set; }

        public DateTime? SnoozedTo { get; set; }
    }
}
