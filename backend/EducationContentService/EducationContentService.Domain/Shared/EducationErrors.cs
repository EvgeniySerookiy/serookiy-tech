namespace EducationContentService.Domain.Shared;

public static class EducationErrors
{
    public static Error TitleConflict(string title)
    {
        return Error.Conflict(
            "lesson.title.conflict",
            $"The lesson with the title {title} already exists",
            "Lesson");
    }

    public static Error DatabaseError()
    {
        return Error.Failure(
            "education.database.error",
            "Database error when working with the lesson");
    }

    public static Error OperationCancelled()
    {
        return Error.Failure(
            "education.operation.cancelled",
            "The operation was cancelled");
    }
}