namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum RoomType
    {
        [LineIcon(Icon = "icon-real-estate-003")]
        [Display(Name = "Entire house")]
        EntireHouse = 1,

        [LineIcon(Icon = "icon-hotel-restaurant-022")]
        [Display(Name = "Private room")]
        PrivateRoom = 2,

        [LineIcon(Icon = "icon-hotel-restaurant-023")]
        [Display(Name = "Shared space")]
        SharedSpace = 3
    }
}