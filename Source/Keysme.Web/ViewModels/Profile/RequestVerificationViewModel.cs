namespace Keysme.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Data.Models;

    public class RequestVerificationViewModel
    {
        public HttpPostedFileBase Front { get; set; }

        public HttpPostedFileBase Back { get; set; }

        [Required]
        public VerificationType? VerificationType { get; set; }

        [Required]
        public CountryCode? CountryCode { get; set; }

        public bool HasRequestedVerification { get; set; }

        public string FrontImagePath { get; set; }

        public string BackImagePath { get; set; }
    }
}