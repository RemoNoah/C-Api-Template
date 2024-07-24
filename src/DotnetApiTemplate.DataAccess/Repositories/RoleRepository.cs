using DotnetApiTemplate.DataAccess.Context;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Repositories;

namespace DotnetApiTemplate.DataAccess.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Role"/> entities, implementing the <see cref="IRoleRepository"/> interface.
/// </summary>
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RoleRepository(DotnetApiTemplateContext dbContext) : base(dbContext) { }
}
