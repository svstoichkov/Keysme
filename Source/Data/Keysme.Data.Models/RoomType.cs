namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum RoomType
    {
        [Display(Name = "Entire house")]
        EntireHouse = 1,

        [Display(Name = "Private room")]
        PrivateRoom = 2,

        [Display(Name = "Shared space")]
        SharedSpace = 3
    }
}