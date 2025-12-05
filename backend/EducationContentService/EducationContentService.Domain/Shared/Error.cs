namespace EducationContentService.Domain.Shared;

public record Error
{
    public IReadOnlyCollection<ErrorMessage> Messages { get; } = [];
    public ErrorType Type { get; }

    private Error(
        IEnumerable<ErrorMessage> messages,
        ErrorType type)
    {
        Messages = messages.ToList();
        Type = type;
    }

    public static Error Validation(params ErrorMessage[] messages) =>
        new(messages, ErrorType.VALIDATION);

    public static Error NotFound(params ErrorMessage[] messages) =>
        new(messages, ErrorType.NOT_FOUND);

    public static Error Failure(params ErrorMessage[] messages) =>
        new(messages, ErrorType.FAILURE);

    public static Error Conflict(params ErrorMessage[] messages) =>
        new(messages, ErrorType.CONFLICT);

    public static Error Authentication(params ErrorMessage[] messages) =>
        new(messages, ErrorType.AUTHENTICATION);

    public static Error Authorization(params ErrorMessage[] messages) =>
        new(messages, ErrorType.AUTHORIZATION);
}

public record ErrorMessage(
    string Code,
    string Message,
    string? InvalidField = null);

public enum ErrorType
{
    VALIDATION,
    NOT_FOUND,
    FAILURE,
    CONFLICT,
    AUTHENTICATION,
    AUTHORIZATION,
}