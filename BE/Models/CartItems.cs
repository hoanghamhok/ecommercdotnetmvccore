using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MYWEBAPI.Models
{
    [Table("CartItems")]
public class CartItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartItemId { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }  // Navigation property

    [Required]
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Products Product { get; set; }  // Navigation property

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Có thể thêm các trường sau nếu cần:
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }  // Giá tại thời điểm thêm vào giỏ hàng

    [MaxLength(50)]
    public string? Notes { get; set; }  // Ghi chú cho sản phẩm
}
}