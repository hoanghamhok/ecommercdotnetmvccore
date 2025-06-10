using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MYWEBAPI.Models
{
    [Table("OrdersDetails")]
public class OrdersDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderDetailId { get; set; }

    [Required]
    [ForeignKey("OrderId")]
    public int OrderId { get; set; }
    public Orders Order { get; set; }  // Navigation property

    [Required]
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Products Product { get; set; }  // Navigation property

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Có thể thêm Discount nếu cần
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Discount { get; set; }
}
}