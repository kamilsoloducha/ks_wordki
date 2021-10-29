using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    internal class UpdateGroupNameNameRule : IBuissnessRule
    {
        private readonly string _groupName;

        public string Message { get; } = "Group name cannot be empty";

        public UpdateGroupNameNameRule(string groupName)
        {
            _groupName = groupName;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(!string.IsNullOrEmpty(_groupName));
    }
}