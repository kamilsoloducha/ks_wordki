using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    internal class SetSidesSidesRule : IBuissnessRule
    {
        private readonly IEnumerable<CardSide> _sides;
        private readonly Side _side;
        public string Message { get; }

        public SetSidesSidesRule(IEnumerable<CardSide> sides, Side side)
        {
            _sides = sides;
            _side = side;
            Message = $"Card must have {_side.ToString()} side";
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_sides.Any(x => x.Side == _side));
    }
}