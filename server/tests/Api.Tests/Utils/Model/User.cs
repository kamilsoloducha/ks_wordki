using System;
using System.Collections.Generic;

namespace Api.Tests.Utils
{
    public class UserTest
    {
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
        public IList<RoleTest> Roles { get; set; }
    }

    public class RoleTest
    {
        public int Id { get; private set; }
        public int Type { get; private set; }
        public UserTest User { get; private set; }
    }
}