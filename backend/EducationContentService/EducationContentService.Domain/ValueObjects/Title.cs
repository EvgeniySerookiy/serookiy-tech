using CSharpFunctionalExtensions;
using EducationContentService.Domain.Shared;

namespace EducationContentService.Domain.ValueObjects;

public record Title
{
    private const int MAX_LENGTH = 200;
    public string Prefix { get; }
    public string Value { get; }

    private Title(string value, string prefix)
    {
        Value = value;
        Prefix = prefix;
    }

    public static Result<Title, Error> Create(string value, string prefix)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return GeneralErrors.Empty(prefix, nameof(Title));
        }

        if (value.Length > MAX_LENGTH)
        {
            return GeneralErrors.TooLong(prefix, MAX_LENGTH, nameof(Title));
        }

        return new Title(value, prefix);
    }
}