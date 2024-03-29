﻿using System;

namespace E2e.Model.Tests.Model.Users;

public partial class Role
{
    public int Id { get; set; }
    public int Type { get; set; }
    public Guid? UserId { get; set; }

    public virtual User User { get; set; }
}