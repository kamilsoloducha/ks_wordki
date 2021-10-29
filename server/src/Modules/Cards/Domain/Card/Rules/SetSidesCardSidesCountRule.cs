using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    internal class SetSidesCardSidesCountRule : IBuissnessRule
    {
        private readonly IEnumerable<CardSide> _sides;

        public string Message { get; } = "Card must have 2 sides";

        public SetSidesCardSidesCountRule(IEnumerable<CardSide> sides)
        {
            _sides = sides;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_sides.Count() == 2);
    }
}