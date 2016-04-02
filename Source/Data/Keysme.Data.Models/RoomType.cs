namespace Keysme.Data.Models
{
    using System.ComponentModel;

    public enum RoomType
    {
        [Description("Entire house")]
        EntireHouse = 0,

        [Description("Private room")]
        PrivateRoom = 1,

        [Description("Shared space")]
        SharedSpace = 2
    }
}