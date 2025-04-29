using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MYWEBAPI.Models{
    public class User{
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(100)]

        public string Username { get; set; } = string.Empty;
        [Required]
        [Column("PasswordHash")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string? Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [RegularExpression("Admin|Customer", ErrorMessage = "Role must be 'Admin' or 'Customer'")]
        public string Role  { get; set; } = "Customer";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

