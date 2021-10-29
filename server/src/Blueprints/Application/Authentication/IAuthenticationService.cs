using System;
using System.Collections.Generic;

namespace Blueprints.Application.Authentication
{
    public interface IAuthenticationService
    {
        string Authenticate(Guid userId, IEnumerable<string> roles);
        string Refresh(string token);
    }
}