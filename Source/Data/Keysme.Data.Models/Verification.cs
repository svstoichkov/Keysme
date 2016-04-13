namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Verification
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        
        public virtual User User { get; set; }

        public VerificationType Type { get; set; }

        [Required]
        [MaxLength(300)]
        public string FrontPicture { get; set; }

        [Required]
        [MaxLength(300)]
        public string BackPicture { get; set; }

        [Required]
        public CountryCode CountryCode { get; set; }

        public bool IsApproved { get; set; }
    }
}
