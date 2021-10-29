namespace Users.Application
{
    public interface IPasswordManager
    {
        string CreateHashedPassword(string password);
    }
}