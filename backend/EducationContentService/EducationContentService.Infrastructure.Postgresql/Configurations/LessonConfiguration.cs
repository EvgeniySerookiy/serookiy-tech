using EducationContentService.Domain.Lessons;
using EducationContentService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationContentService.Infrastructure.Postgresql.Configurations;

public static class Index
{
    public const string TITLE = "ix_lessons_title";
}

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("lessons");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Title)
            .HasConversion(
                v => v.ToString(),
                v => Title.Create(v).Value)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasConversion(
                v => v.ToString(),
                v => Description.Create(v).Value)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("timezone('utc', now())")
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasIndex(x => x.Id).IsUnique().HasDatabaseName(Index.TITLE);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}