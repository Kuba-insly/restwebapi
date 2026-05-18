using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restwebapi.Models;

[Table("Components")]
public class Components
{
    [Key, MaxLength(10)]
    public string Code { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ComponentManufacturersId  { get; set; }
    public int ComponentTypeId { get; set; }

    public ICollection<PCComponents> PCComponents { get; set; } = [];
    
    [ForeignKey(nameof(ComponentManufacturersId))]
    public ComponentManufacturers ComponentManufacturers { get; set; }
    [ForeignKey(nameof(ComponentTypeId))]
    public ComponentTypes ComponentTypes { get; set; }
}