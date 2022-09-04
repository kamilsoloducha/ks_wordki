using System.Security.Cryptography;
using System.Text;
using Users.Application;
using Users.Application.Services;

namespace Users.Infrastructure.Services;

public class PasswordManager : IPasswordManager
{
    public string CreateHashedPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return string.Empty;
        }
        var data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in data)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}