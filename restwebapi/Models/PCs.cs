using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restwebapi.Models;

[Table("PCs")]
public class PCs
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public ICollection<PCComponents> PCComponents { get; set; } = [];
}