using Cards.Domain.ValueObjects;

namespace Cards.Domain.Commands
{
    public record AddCardCommand(Label FrontValue,
        Label BackValue,
        Example FrontExample,
        Example BackExample,
        Comment FrontComment,
        Comment BackComment,
        bool FrontIsUsed,
        bool BackIsUsed);
}