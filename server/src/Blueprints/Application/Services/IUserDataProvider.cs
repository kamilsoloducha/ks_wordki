using System;

namespace Blueprints.Application.Services
{
    public interface IUserDataProvider
    {
        Guid GetUserId();
        bool IsAdmin();
    }
}