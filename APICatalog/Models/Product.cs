using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalog.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

}
