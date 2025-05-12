using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>().ToTable("Projects");
        modelBuilder.Entity<StatusEntity>().ToTable("Statuses");
        modelBuilder.Entity<ClientEntity>().ToTable("Clients");
    }

    public DbSet<ProjectEntity> Projects { get; set; } = null!;
}