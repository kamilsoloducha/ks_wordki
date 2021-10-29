using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;
using Domain.IntegrationEvents;

namespace Users.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public DateTime CreationDate { get; private set; }
        public RegistrationStatus Status { get; private set; }
        public DateTime ConfirmationDate { get; private set; }
        public DateTime LoginDate { get; private set; }
        public ICollection<Role> Roles { get; private set; }

        private User()
        {
            Roles = new List<Role>();
        }

        public static async Task<User> RegisterUser(
            string name,
            string password,
            string email,
            string firstName,
            string surname,
            IDataChecker dataChecker,
            CancellationToken cancellationToken)
        => await Register(name, password, email, firstName, surname, new[] { Role.Student }, dataChecker, cancellationToken);

        public static async Task<User> RegisterAdmin(
            string name,
            string password,
            string email,
            string firstName,
            string surname,
            IDataChecker dataChecker,
            CancellationToken cancellationToken)
        => await Register(name, password, email, firstName, surname, new[] { Role.Admin, Role.Student }, dataChecker, cancellationToken);

        protected static async Task<User> Register(
            string name,
            string password,
            string email,
            string firstName,
            string surname,
            IEnumerable<Role> roles,
            IDataChecker dataChecker,
            CancellationToken cancellationToken)
        {
            await CheckRule(new RegisterLoginRule(dataChecker, name), cancellationToken);
            await CheckRule(new RegisterEmailRule(dataChecker, name), cancellationToken);

            var newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.Name = name;
            newUser.Password = password;
            newUser.Email = email;
            newUser.FirstName = firstName;
            newUser.Surname = surname;
            newUser.Status = RegistrationStatus.WaitingForConfirmation;
            newUser.CreationDate = DateTime.UtcNow;
            foreach (var role in roles)
            {
                newUser.AddRole(role);
            }

            return newUser;
        }

        public void Confirm()
        {
            CheckRule(new ConfirmUserStatusRule(Status));

            Status = RegistrationStatus.Registered;
            ConfirmationDate = DateTime.UtcNow;
            _events.Add(new UserCreated { Id = Id });
        }

        public void Remove()
        {
            CheckRule(new RemoveUserStatusRule(Status));
            _events.Add(new UserRemoved { Id = Id });

            Status = RegistrationStatus.Removed;
        }

        public void Login()
        {
            CheckRule(new LoginUserStatusRule(Status));

            LoginDate = DateTime.UtcNow;
        }

        private void AddRole(Role role)
        {
            Roles.Add(role);
        }
    }
}

