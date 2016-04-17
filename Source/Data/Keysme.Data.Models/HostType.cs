namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum HostType
    {
        [LineIcon(Icon = "icon-hotel-restaurant-144")]
        Hotel = 1,

        [LineIcon(Icon = "icon-real-estate-002")]
        Motel = 2,

        [LineIcon(Icon = "icon-hotel-restaurant-023")]
        Hostel = 3,

        [LineIcon(Icon = "icon-hotel-restaurant-074")]
        [Display(Name = "Bed & Breakfast")]
        BAndB = 4,

        [LineIcon(Icon = "icon-real-estate-042")]
        Apartment = 5,

        [LineIcon(Icon = "icon-real-estate-043")]
        House = 6,

        [LineIcon(Icon = "icon-hotel-restaurant-139")]
        Unique = 7
    }
}