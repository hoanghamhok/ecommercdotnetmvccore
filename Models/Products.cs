using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("Products")]
public class Products
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ProductName { get; set; }

    [Column(TypeName = "nvarchar(MAX)")] // Cho phép text dài
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Categories Category { get; set; }

    // Navigation properties
    public ICollection<Reviews> Reviews { get; set; }
    public ICollection<CartItems> CartItems { get; set; }
    public ICollection<OrdersDetails> OrderDetails { get; set; }
}
}