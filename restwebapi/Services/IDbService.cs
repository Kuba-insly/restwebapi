using restwebapi.DTOs;

namespace restwebapi.Services;

public interface IDbService
{
    Task<IEnumerable<GetPCDto>> GetAllPCs();
    Task<GetPCByIdDto> GetPCById(int id);
    Task<GetPCDto> AddPC(AddPCDto dto);
    Task UpdatePC(int id, UpdatePCDto dto);
    Task DeletePC(int id);
}