using AutoMapper;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Domain.UnitOfWork;

namespace DotnetApiTemplate.Services.Services;

/// <summary>
/// Service class for managing user-related operations.
/// This class implements the <see cref="IAuthService"/> interface.
/// </summary>
/// <param name="unitOfWork"> <see cref="UnitOfWork"/> </param>
public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService 
{ 
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId()
    {
        IEnumerable<RoleWithoutIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(r => _mapper.Map<RoleWithoutIdDTO>(r));
        return roles;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithIdDTO>> GetAllWithId()
    {
        IEnumerable<RoleWithIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(r => _mapper.Map<RoleWithIdDTO>(r));
        return roles;
    }

    /// <inheritdoc/>
    public async Task<Guid> GetIdByName(string name)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == name);
        return role!.Id;
    }

    /// <inheritdoc/>
    public async Task<string> GetNameById(Guid id)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Id == id);
        return role!.Name;
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Delete(Guid roleId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Update(RoleWithIdDTO role)
    {
        throw new NotImplementedException();
    }
}
