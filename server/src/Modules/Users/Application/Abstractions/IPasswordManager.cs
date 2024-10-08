namespace Users.Application.Services;

public interface IPasswordManager
{
    string CreateHashedPassword(string password);
}