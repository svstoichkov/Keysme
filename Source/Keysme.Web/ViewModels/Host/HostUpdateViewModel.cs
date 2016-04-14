namespace Keysme.Web.ViewModels.Host
{
    public class HostUpdateViewModel
    {
        public int HostId { get; set; }

        public HostMainInformationViewModel HostMainInformation { get; set; }

        public AmenitiesViewModel Amenities { get; set; }
    }
}