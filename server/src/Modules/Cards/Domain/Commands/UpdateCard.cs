using Cards.Domain.ValueObjects;

namespace Cards.Domain.Commands
{
    public record UpdateCard(Side Front, Side Back, bool IsTicked);

    public record Side(Label Label, Example Example, string Comment, bool UseAsQuestion);
}
