namespace EducationContentService.Domain.Shared;

public static class GeneralErrors
{
    public static Error Empty(string prefix, string field)
    {
        return Error.Validation(
            $"{prefix.ToLower()}.{field.ToLower()}.empty",
            $"The {field.ToLower()} cannot be empty",
            $"{field}");
    }

    public static Error TooLong(string prefix, int maxLength, string field)
    {
        return Error.Validation(
            $"{prefix.ToLower()}.{field.ToLower()}.too_long",
            $"The {field.ToLower()} cannot be longer than {maxLength} characters",
            $"{field}");
    }
}