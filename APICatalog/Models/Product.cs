using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(80)]
        [StringLength(80, 
            ErrorMessage = "Name should have at most {1} and at least {2} characteres", 
            MinimumLength = 5)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(80)]
        [StringLength(80,
            ErrorMessage = "Description should have at most {1} characteres")]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(8,2)")]
        [Range(1, 10000, ErrorMessage = "Price should be between {1} and {2}")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }

}
