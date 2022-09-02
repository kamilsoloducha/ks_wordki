#nullable disable

using System;

namespace Users.E2e.Tests.Models.Users;

public partial class Role
{
    public int Id { get; set; }
    public int Type { get; set; }
    public Guid? UserId { get; set; }

    public virtual User User { get; set; }
}