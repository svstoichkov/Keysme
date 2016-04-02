namespace Keysme.Data.Models
{
    using System.ComponentModel;

    public enum HostType
    {
        Hotel = 0,
        Hostel = 1,

        [Description("B&B")]
        BAndB = 2,
        Apartment = 3,
        House = 4
    }
}