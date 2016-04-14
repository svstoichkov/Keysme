namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum HostType
    {
        Hotel = 1,
        Motel = 2,
        Hostel = 3,

        [Display(Name = "Bed & Breakfast")]
        BAndB = 4,
        Apartment = 5,
        House = 6
    }
}