namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }

        public int HostId { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string Filename { get; set; }
    }
}
