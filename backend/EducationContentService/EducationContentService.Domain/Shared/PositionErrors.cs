using EducationContentService.Domain.ModuleItems;

namespace EducationContentService.Domain.Shared;

public static class PositionErrors
{
    public static Error TypeMismatch() =>
        Error.Validation(new ErrorMessage(
            "position.type.mismatch",
            "The item types don't match",
            nameof(Position.Type)));

    public static Error OrderInvalid() =>
        Error.Validation(new ErrorMessage(
            "position.order.invalid",
            "The position before is greater than the position after",
            nameof(Position.Value)));
}