namespace EducationContentService.Domain.ModuleItems;

public sealed class ModuleItem
{
    public Guid Id { get; private set; }

    public Guid ModuleId { get; private set; }

    public ItemReference ItemReference { get; private set; } = null!;

    public Position Position { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public ModuleItem(
        Guid? id,
        Guid moduleId,
        ItemReference itemReference,
        Position position)
    {
        Id = id ?? Guid.NewGuid();
        ModuleId = moduleId;
        ItemReference = itemReference;
        Position = position;
        CreatedAt = DateTime.UtcNow;
        UpdateAt = CreatedAt;
        DeletedAt = null;
    }

    // EF Core
    private ModuleItem() { }
}