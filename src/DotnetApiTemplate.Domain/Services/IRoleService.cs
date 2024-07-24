using DotnetApiTemplate.Domain.DTO.Role;

namespace DotnetApiTemplate.Domain.Services;

/// <summary>
/// Interface for <see cref="IRoleService"/>.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Gets all roles without Id.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId();

    /// <summary>
    /// Gets all roles with Id.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithIdDTO>> GetAllWithId();

    /// <summary>
    /// Gets the Id by the Name of the Role.
    /// </summary>
    /// <param name="Name">The Name of the Role</param>
    /// <returns>The Id of the role with the given Name</returns>
    Task<Guid> GetIdByName(string Name);

    /// <summary>
    /// Gets the Name of the role by the Name.
    /// </summary>
    /// <param name="Id">The Id of the Role</param>
    /// <returns>The Name of the role with the given Id</returns>
    Task<string> GetNameById(Guid Id);

    /// <summary>
    /// Updates the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The updated Role</returns>
    Task<RoleWithoutIdDTO> Update(RoleWithIdDTO role);

    /// <summary>
    /// Creates the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The created Role</returns>
    Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role);

    /// <summary>
    /// Delete the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The created Role</returns>
    Task<RoleWithoutIdDTO> Delete(Guid roleId);
}
