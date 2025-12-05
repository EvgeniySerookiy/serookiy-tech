using CSharpFunctionalExtensions;
using EducationContentService.Domain.Shared;

namespace EducationContentService.Domain.ValueObjects;

public record Description
{
    private const int MAX_LENGTH = 1000;
    public string Prefix { get; }
    public string Value { get; }

    private Description(string value,  string prefix)
    {
        Value = value;
        Prefix = prefix;
    }

    public static Result<Description, Error> Create(string value, string prefix)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return GeneralErrors.Empty(prefix, nameof(Description));
        }

        if (value.Length > MAX_LENGTH)
        {
            return GeneralErrors.TooLong(prefix, MAX_LENGTH, nameof(Description));
        }

        return new Description(value, prefix);
    }
}