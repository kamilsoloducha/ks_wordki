using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Users.Application.Services;
using Users.Domain.User;

namespace Users.Infrastructure.Services.AdminUserCreator;

public class AdminAccountCreator
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly AdminAccountConfiguration _adminAccountSettings;

    public AdminAccountCreator(IUserRepository userRepository,
        IOptions<AdminAccountConfiguration> options,
        IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _adminAccountSettings = options.Value;
    }

    public async Task CreateAdminUser()
    {
        var isAdminAccountExists = await _userRepository.Any(x => x.Name == _adminAccountSettings.UserName, CancellationToken.None);
        if (isAdminAccountExists) return;

        var adminUser = User.RegisterAdmin(
            _adminAccountSettings.UserName,
            _passwordManager.CreateHashedPassword(_adminAccountSettings.Password),
            _adminAccountSettings.Email,
            null,
            null);

        adminUser.Confirm();

        await _userRepository.Add(adminUser, CancellationToken.None);
    }
}