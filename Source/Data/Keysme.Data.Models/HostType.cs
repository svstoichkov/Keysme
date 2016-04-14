namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum HostType
    {
        Hotel = 1,
        Hostel = 2,

        [Display(Name = "Bed & Breakfast")]
        BAndB = 3,
        Apartment = 4,
        House = 5
    }
}