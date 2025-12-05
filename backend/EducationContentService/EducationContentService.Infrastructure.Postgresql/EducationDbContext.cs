using EducationContentService.Domain.Lessons;
using EducationContentService.Domain.ModuleItems;
using EducationContentService.Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace EducationContentService.Infrastructure.Postgresql;

public class EducationDbContext : DbContext
{
    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<Module> Modules => Set<Module>();

    public DbSet<ModuleItem> ModuleItems => Set<ModuleItem>();

    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EducationDbContext).Assembly);
    }
}