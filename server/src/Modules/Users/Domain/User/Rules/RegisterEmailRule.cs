using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Rules;
using Users.Domain.User.Services;

namespace Users.Domain.User.Rules
{
    public class RegisterEmailRule : IBuissnessRule
    {

        private readonly Expression<Func<User, bool>> AnyEmailExpression;
        private readonly IDataChecker _dataChecker;
        private readonly string _email;
        public string Message { get; } = "Email is already used";

        public RegisterEmailRule(IDataChecker dataChecker, string email)
        {
            _dataChecker = dataChecker;
            _email = email;
            AnyEmailExpression = i => _email == i.Email;
        }

        public async Task<bool> IsCorrect(CancellationToken cancellationToken)
            => !(await _dataChecker.Any(AnyEmailExpression, cancellationToken));
    }
}