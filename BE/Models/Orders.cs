using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MYWEBAPI.Models
{
    [Table("Orders")]
public class Orders
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    
    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }  // Sử dụng decimal thay vì int
    
    [Required]
    [StringLength(20)]
    public string OrderStatus { get; set; } = "Pending";
    
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Nên dùng UtcNow
    
    // Quan hệ với OrderDetails (nếu có)
    public ICollection<OrdersDetails> OrderDetails { get; set; }
}
}