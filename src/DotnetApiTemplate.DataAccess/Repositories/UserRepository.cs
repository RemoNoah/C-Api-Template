using DotnetApiTemplate.DataAccess.Context;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Repositories;

namespace DotnetApiTemplate.DataAccess.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="User"/> entities, implementing the <see cref="IUserRepository"/> interface.
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRepository(DotnetApiTemplateContext dbContext) : base(dbContext) { }
}
