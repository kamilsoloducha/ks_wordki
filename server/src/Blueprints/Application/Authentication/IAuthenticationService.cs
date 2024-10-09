using System;
using System.Collections.Generic;

namespace Application.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Authenticate(Guid userId, IEnumerable<string> roles);
    AuthenticationResult Refresh(string token);
}

public record AuthenticationResult(string Token, DateTime CreatingDateTime, DateTime ExpirationDateTime);