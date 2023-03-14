using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Rules;

namespace Domain
{
    public abstract class Entity
    {
        protected List<object> _events;
        public IEnumerable<object> Events => _events.AsEnumerable();
        public bool IsNew { get; protected set; }
        public bool IsDirty { get; protected set; }

        protected Entity()
        {
            _events = new List<object>();
        }

        protected static async Task CheckRule(IBuissnessRule rule, CancellationToken cancellationToken)
        {
            if (await rule.IsCorrect(cancellationToken)) return;
            throw new BuissnessRuleFailedException(rule);
        }

        protected static void CheckRule(IBuissnessRule rule)
        {
            if (rule.IsCorrect(CancellationToken.None).Result) return;
            throw new BuissnessRuleFailedException(rule);
        }
    }
}