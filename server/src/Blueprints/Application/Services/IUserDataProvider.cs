using System;

namespace Application.Services;

public interface IUserDataProvider
{
    Guid GetUserId();
    bool IsAdmin();
}