using System.Threading;
using System.Threading.Tasks;
using Users.Domain;
using Microsoft.Extensions.Options;
using Users.Application;

namespace Users.Infrastructure
{
    public class AdminAccountCreator
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataChecker _dataChecker;
        private readonly IPasswordManager _passwordManager;
        private readonly AdminAccountConfiguration _adminAccountSettings;

        public AdminAccountCreator(IUserRepository userRepository,
            IDataChecker dataChecker,
            IOptions<AdminAccountConfiguration> options,
            IPasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _dataChecker = dataChecker;
            _passwordManager = passwordManager;
            _adminAccountSettings = options.Value;
        }

        public async Task CreateAdminUser()
        {
            var isAdminAccountExists = await _userRepository.Any(x => x.Name == _adminAccountSettings.UserName, CancellationToken.None);
            if (isAdminAccountExists) return;

            var adminUser = await User.RegisterAdmin(
                _adminAccountSettings.UserName,
                _passwordManager.CreateHashedPassword(_adminAccountSettings.Password),
                _adminAccountSettings.Email,
                null,
                null,
                _dataChecker,
                CancellationToken.None);

            adminUser.Confirm();

            await _userRepository.Add(adminUser, CancellationToken.None);
        }
    }

}
