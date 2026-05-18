using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace restwebapi.Models;

[Table("PCComponents")]
[PrimaryKey(nameof(PCId), nameof(ComponentCode))]
public class PCComponents
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; } = string.Empty;
    public int Amount { get; set; }
    
    [ForeignKey(nameof(PCId))]
    public PCs PCs { get; set; }
    [ForeignKey(nameof(ComponentCode))]
    public Components Components { get; set; }
}