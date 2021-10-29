namespace Users.Domain
{
    public class Role
    {
        public static Role Admin => new Role() { Type = RoleType.Admin };
        public static Role Student => new Role() { Type = RoleType.Student };

        public int Id { get; private set; }
        public RoleType Type { get; private set; }
        public User User { get; private set; }

        private Role() { }
    }
}