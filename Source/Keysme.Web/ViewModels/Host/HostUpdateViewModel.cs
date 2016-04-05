namespace Keysme.Web.ViewModels.Host
{
    using Models.Host;

    public class HostUpdateViewModel
    {
        public int HostId { get; set; }

        public HostViewModel Host { get; set; }

        public AmenitiesViewModel Amenities { get; set; }
    }
}