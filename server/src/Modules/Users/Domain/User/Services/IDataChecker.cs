using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Domain.User.Services;

public interface IDataChecker
{
    Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);
}

public class DataChecker : IDataChecker
{
    private readonly IUserRepository _userRepository;

    public DataChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
        => await _userRepository.Any(expression, cancellationToken);
}