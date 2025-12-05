namespace EducationContentService.Domain.Shared;

public static class GeneralErrors
{
    public static Error Empty(string prefix, string field)
    {
        return Error.Validation(new ErrorMessage(
            $"{prefix.ToLower()}.title.empty",
            $"The {field} cannot be empty",
            "Title"));
    }

    public static Error TooLong(string prefix, int maxLength, string field)
    {
        return Error.Validation(new ErrorMessage(
            $"{prefix.ToLower()}.title.too_long",
            $"The {field} cannot be longer than {maxLength} characters",
            "Title"));
    }
}