namespace Users.Domain
{
    public class Role
    {
        public static Role Admin => new() { Type = RoleType.Admin };
        public static Role Student => new() { Type = RoleType.Student };
        public static Role ChromeExtension => new() { Type = RoleType.ChromeExtension };

        public int Id { get; private set; }
        public RoleType Type { get; private set; }
        public User User { get; private set; }

        private Role()
        {
        }
    }
}