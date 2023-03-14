using System;
using System.Collections.Generic;

namespace Application.Authentication
{
    public interface IAuthenticationService
    {
        string Authenticate(Guid userId, IEnumerable<string> roles);
        string Refresh(string token);
    }
}