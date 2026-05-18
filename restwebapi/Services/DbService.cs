using Microsoft.EntityFrameworkCore;
using restwebapi.Data;
using restwebapi.DTOs;
using restwebapi.Exceptions;
using restwebapi.Models;

namespace restwebapi.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;
    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPCDto>> GetAllPCs()
    {
        return await _context.PCs.Select(e => new GetPCDto
        {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock
        }).ToListAsync();
    }

    public async Task<GetPCByIdDto> GetPCById(int id)
    {
        var res = await _context.PCs
            .Where(p => p.Id == id)
            .Select(p => new GetPCByIdDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock,
                Components = p.PCComponents.Select(pc => new PCComponentDto
                {
                    Amount = pc.Amount,
                    Component = new ComponentDto
                    {
                        Code = pc.Components.Code,
                        Name = pc.Components.Name,
                        Description = pc.Components.Description,
                        Manufacturer = new ManufacturerDto
                        {
                            Id = pc.Components.ComponentManufacturers.Id,
                            Abbreviation = pc.Components.ComponentManufacturers.Abbreviation,
                            FullName = pc.Components.ComponentManufacturers.FullName,
                            FoundationDate = pc.Components.ComponentManufacturers.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pc.Components.ComponentTypes.Id,
                            Abbreviation = pc.Components.ComponentTypes.Abbreviation,
                            Name = pc.Components.ComponentTypes.Name
                        }
                    }
                })
            })
            .FirstOrDefaultAsync();

        if (res == null)
            throw new NotFoundException($"PC with id {id} not found");

        return res;
    }

    public async Task<GetPCDto> AddPC(AddPCDto dto)
    {
        var pc = new PCs
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };
        await _context.PCs.AddAsync(pc);
        await _context.SaveChangesAsync();

        return new GetPCDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task UpdatePC(int id, UpdatePCDto dto)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
            throw new NotFoundException($"PC with id {id} not found");

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
    }

    public async Task DeletePC(int id)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null)
            throw new NotFoundException($"PC with id {id} not found");

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
    }
}