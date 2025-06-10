using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MYWEBAPI.Models
{
    [Table("Payments")]
public class Payments
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }

    [Required]
    [ForeignKey("OrderId")]
    public int OrderId { get; set; }
    public Orders Order { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; set; }

    [Required]
    [MaxLength(20)]
    public string PaymentStatus { get; set; }

    [Required]
    [MaxLength(100)]
    public string TransactionId { get; set; }

    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
}