using Microsoft.AspNetCore.Mvc;
using restwebapi.DTOs;
using restwebapi.Exceptions;
using restwebapi.Services;

namespace restwebapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PCsController : ControllerBase
{
    private readonly IDbService _dbService;
    public PCsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPCs()
    {
        var pcs = await _dbService.GetAllPCs();
        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPCById(int id)
    {
        try
        {
            var pc = await _dbService.GetPCById(id);
            return Ok(pc);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPC(AddPCDto dto)
    {
        var pc = await _dbService.AddPC(dto);
        return CreatedAtAction(nameof(GetPCById), new { id = pc.Id }, pc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePC(int id, UpdatePCDto dto)
    {
        try
        {
            await _dbService.UpdatePC(id, dto);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePC(int id)
    {
        try
        {
            await _dbService.DeletePC(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}