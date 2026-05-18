using Microsoft.EntityFrameworkCore;
using restwebapi.Models;

namespace restwebapi.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<ComponentManufacturers> ComponentManufacturers { get; set; }
    public DbSet<ComponentTypes> ComponentTypes { get; set; }
    public DbSet<Components> Components { get; set; }
    public DbSet<PCComponents> PCComponents { get; set; }
    public DbSet<PCs> PCs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturers>().HasData(new List<ComponentManufacturers>
        {
            new() { Id = 1, Abbreviation = "AMD",  FullName = "Advanced Micro Devices",  FoundationDate = new DateTime(1969, 5, 1) },
            new() { Id = 2, Abbreviation = "NV",   FullName = "NVIDIA Corporation",       FoundationDate = new DateTime(1993, 4, 5) },
            new() { Id = 3, Abbreviation = "COR",  FullName = "Corsair Gaming Inc.",      FoundationDate = new DateTime(1994, 1, 1) }
        });

        modelBuilder.Entity<ComponentTypes>().HasData(new List<ComponentTypes>
        {
            new() { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new() { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new() { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        });

        modelBuilder.Entity<Components>().HasData(new List<Components>
        {
            new() { Code = "CPU0000001", Name = "Ryzen 7 7800X3D",          Description = "8-core gaming processor",          ComponentManufacturersId = 1, ComponentTypeId = 1 },
            new() { Code = "GPU0000001", Name = "RTX 4080 Super",            Description = "High-end gaming graphics card",    ComponentManufacturersId = 2, ComponentTypeId = 2 },
            new() { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB",           ComponentManufacturersId = 3, ComponentTypeId = 3 }
        });

        modelBuilder.Entity<PCs>().HasData(new List<PCs>
        {
            new() { Id = 1, Name = "Gaming Beast X",    Weight = 12.5f, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8,  9,  0, 0), Stock = 5  },
            new() { Id = 2, Name = "Office Mini Pro",   Weight = 4.2f,  Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new() { Id = 3, Name = "Home Workstation",  Weight = 8.0f,  Warranty = 24, CreatedAt = new DateTime(2026, 3, 1,  10, 0, 0),  Stock = 7  }
        });

        modelBuilder.Entity<PCComponents>().HasData(new List<PCComponents>
        {
            new() { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new() { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new() { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            new() { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 },
            new() { PCId = 2, ComponentCode = "RAM0000001", Amount = 1 },
            new() { PCId = 3, ComponentCode = "GPU0000001", Amount = 1 },
            new() { PCId = 3, ComponentCode = "RAM0000001", Amount = 2 }
        });
        
        base.OnModelCreating(modelBuilder);
    }
}