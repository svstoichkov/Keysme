namespace Keysme.Web.ViewModels.Profile
{
    using System.Web;

    using Data.Models;

    public class RequestVerificationViewModel
    {
        public HttpPostedFileBase Front { get; set; }

        public HttpPostedFileBase Back { get; set; }

        public VerificationType VerificationType { get; set; }

        public CountryCode CountryCode { get; set; }
    }
}