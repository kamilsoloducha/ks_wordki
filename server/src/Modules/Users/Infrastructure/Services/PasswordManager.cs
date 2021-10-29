using System.Security.Cryptography;
using System.Text;
using Users.Application;

namespace Users.Infrastructure
{
    public class PasswordManager : IPasswordManager
    {
        public string CreateHashedPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}