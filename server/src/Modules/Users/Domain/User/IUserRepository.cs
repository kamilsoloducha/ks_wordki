using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Domain
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid id, CancellationToken cancellationToken);
        Task<User> GetUser(string name, string password, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken);
        Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);

        Task Add(User user, CancellationToken cancellationToken);
        Task Update(User user, CancellationToken cancellationToken);
        Task Remove(User user, CancellationToken cancellationToken);
    }
}
