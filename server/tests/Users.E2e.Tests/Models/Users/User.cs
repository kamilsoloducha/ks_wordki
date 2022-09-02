#nullable disable

using System;
using System.Collections.Generic;

namespace Users.E2e.Tests.Models.Users;

public partial class User
{
    public User()
    {
        Roles = new HashSet<Role>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateTime CreationDate { get; set; }
    public int Status { get; set; }
    public DateTime ConfirmationDate { get; set; }
    public DateTime LoginDate { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
}