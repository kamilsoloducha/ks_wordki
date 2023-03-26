using System;
using System.Collections.Generic;
using Domain;
using Domain.IntegrationEvents;
using Domain.Utils;
using Users.Domain.User.Rules;

namespace Users.Domain.User;

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
    public ICollection<Role.Role> Roles { get; private set; }

    private User()
    {
        Roles = new List<Role.Role>();
    }

    public static User RegisterUser(
        string name,
        string password,
        string email,
        string firstName,
        string surname)
        => Register(name, password, email, firstName, surname, new[] { Role.Role.Student });

    public static User RegisterAdmin(
        string name,
        string password,
        string email,
        string firstName,
        string surname)
        => Register(name, password, email, firstName, surname, new[] { Role.Role.Admin, Role.Role.Student });

    protected static User Register(
        string name,
        string password,
        string email,
        string firstName,
        string surname,
        IEnumerable<Role.Role> roles)
    {
        var newUser = new User();
        newUser.Id = Guid.NewGuid();
        newUser.Name = name;
        newUser.Password = password;
        newUser.Email = email;
        newUser.FirstName = firstName;
        newUser.Surname = surname;
        newUser.Status = RegistrationStatus.Registered;
        newUser.ConfirmationDate = SystemClock.Now;
        newUser.CreationDate = SystemClock.Now;
        foreach (var role in roles)
        {
            newUser.AddRole(role);
        }
        newUser._events.Add(new UserCreated { Id = newUser.Id });
        return newUser;
    }

    public void Confirm()
    {
        CheckRule(new ConfirmUserStatusRule(Status));

        Status = RegistrationStatus.Registered;
        ConfirmationDate = SystemClock.Now;
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
        LoginDate = SystemClock.Now;
    }

    private void AddRole(Role.Role role)
    {
        Roles.Add(role);
    }
}