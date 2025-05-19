using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectDbWithDotEnv.EntityFramework.Models;

[Table("USER_LOGIN")]
public class UserLogin
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("ID")]
  public int Id { get; set; }

  [Required]
  [Column("FULL_LOGIN")]
  [StringLength(100)]
  public string FullLogin { get; set; }

  [Required]
  [Column("DATE_ADDED")]
  public DateTime DateAdded { get; set; } = DateTime.Now;

  [Required]
  [Column("LAST_ACCESS")]
  public DateTime LastAccess { get; set; } = DateTime.Now;

  [Column("MANUAL_COMMENT")]
  public string ManualComment { get; set; }

  [Column("FULL_NAME")]
  [StringLength(100)]
  public string FullName { get; set; }
}