using DotnetApiTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetApiTemplate.DataAccess.Context;

/// <summary>
/// Represents the Entity Framework database context for the DotnetApiTemplate application.
/// This class inherits from <see cref="DbContext"/> and provides access to the database entities.
/// </summary>
public class DotnetApiTemplateContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DotnetApiTemplateContext" /> class.
    /// </summary>
    /// <remarks>
    /// See <a href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</a> for more information.
    /// </remarks>
    public DotnetApiTemplateContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DotnetApiTemplateContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public DotnetApiTemplateContext(DbContextOptions<DotnetApiTemplateContext> options) : base(options) { }

    //TODO: Remove conection string out of the file to the app settings
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("server=localhost,1433;uid=sa;pwd=Init1234;database=DotnetApiTemplate;TrustServerCertificate=true");
        }
    }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    public virtual DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    /// <value>The roles.</value>
    public virtual DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" },
            new Role { Id = Guid.NewGuid(), Name = "Client" }
        );
    }
}
