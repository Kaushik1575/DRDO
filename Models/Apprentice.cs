using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprenticeshipManagement.Models;

[Table("apprentices")]
public class Apprentice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("apprentice_id")]
    public string ApprenticeId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("department")]
    public string Department { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    [Column("mobile_number")]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("apprenticeship_password_hash")]
    public string ApprenticeshipPasswordHash { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
}
