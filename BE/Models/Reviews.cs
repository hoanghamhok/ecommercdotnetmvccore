using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MYWEBAPI.Models;

namespace Models
{
    [Table("Reviews")]
public class Reviews
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    [Required]
    [MaxLength(500)]
    public string ReviewText { get; set; }

    [MaxLength(100)] // Bỏ Required vì Comment có thể không bắt buộc
    public string? Comment { get; set; }

    [Required]
    [Range(1, 5)] // Giới hạn rating từ 1-5
    public int Rating { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public Products Product { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
}
}
// Compare this snippet from Models/Products.cs: