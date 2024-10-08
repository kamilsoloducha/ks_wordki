using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Domain.User;
using Users.Infrastructure.DataAccess;

namespace Users.Infrastructure.Repositories;

internal class UserRepository(UsersContext usersContext) : IUserRepository
{
    public async Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
        => await usersContext.Users.FirstOrDefaultAsync(expression, cancellationToken) is not null;

    public async Task<User> GetUser(Guid id, CancellationToken cancellationToken)
        => await usersContext.Users
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    
    public async Task<User> GetUser(string name, string password, CancellationToken cancellationToken)
        => await usersContext.Users
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Name == name && x.Password == password, cancellationToken);

    public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken)
        => await usersContext.Users
            .Include(x => x.Roles)
            .ToListAsync(cancellationToken);

    public async Task Add(User user, CancellationToken cancellationToken)
    {
        await usersContext.AddAsync(user, cancellationToken);
        await usersContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(User user, CancellationToken cancellationToken)
    {
        usersContext.Users.Update(user);
        await usersContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(User user, CancellationToken cancellationToken)
    {
        usersContext.Users.Remove(user);
        await usersContext.SaveChangesAsync(cancellationToken);
    }
}