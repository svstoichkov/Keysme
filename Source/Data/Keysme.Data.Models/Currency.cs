namespace Keysme.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(3)]
        public string Code { get; set; }
        
        [MaxLength(8)]
        public string Symbol { get; set; }

        public bool IsActive { get; set; }
    }
}
