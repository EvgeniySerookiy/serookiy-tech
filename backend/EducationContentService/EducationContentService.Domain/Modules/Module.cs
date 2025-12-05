using EducationContentService.Domain.ValueObjects;

namespace EducationContentService.Domain.Modules;

public sealed class Module
{
    public Guid Id { get; set; }

    public Title Title { get; private set; } = null!;

    public Description Description { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public Module(Guid? id, Title title, Description description)
    {
        Id = id ?? Guid.NewGuid();
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdateAt = CreatedAt;
        IsDeleted = false;
        DeletedAt = null;
    }

    // EF Core
    private Module() { }
}