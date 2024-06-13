using DotnetApiTemplate.DataAccess.Context;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Repositories;

namespace DotnetApiTemplate.DataAccess.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RoleRepository(DotnetApiTemplateContext dbContext) : base(dbContext) { }
}
