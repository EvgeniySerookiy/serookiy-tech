using CSharpFunctionalExtensions;
using EducationContentService.Domain.Shared;

namespace EducationContentService.Domain.ModuleItems;

public record Position
{
    private const decimal INITIAL_STEP = 1000;

    public decimal Value { get; }

    public ItemType Type { get; }

    private Position(ItemType type, decimal value)
    {
        Value = value;
        Type = type;
    }

    public static Position First(ItemType type) => new(type, INITIAL_STEP);

    public static Result<Position, Error> Between(Position before, Position after)
    {
        if (before.Type != after.Type)
        {
            return PositionErrors.TypeMismatch();
        }

        if (before.Value >= after.Value)
        {
            return PositionErrors.OrderInvalid();
        }

        return new Position(before.Type, (after.Value + before.Value) / 2);
    }

    public static Position After(Position previous) => new(previous.Type, previous.Value + INITIAL_STEP);
}