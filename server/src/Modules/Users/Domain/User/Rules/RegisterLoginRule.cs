using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Rules;
using Users.Domain.User.Services;

namespace Users.Domain.User.Rules;

public class RegisterLoginRule : IBuissnessRule
{
    private readonly Expression<Func<User, bool>> AnyLoginExpression;
    private readonly IDataChecker _dataChecker;
    private readonly string _userName;
    public string Message { get; } = "Login is already used";

    public RegisterLoginRule(IDataChecker dataChecker, string userName)
    {
        _dataChecker = dataChecker;
        _userName = userName;
        AnyLoginExpression = i => userName == i.Name;
    }

    public async Task<bool> IsCorrect(CancellationToken cancellationToken)
    {
        return !(await _dataChecker.Any(AnyLoginExpression, cancellationToken));
    }
}