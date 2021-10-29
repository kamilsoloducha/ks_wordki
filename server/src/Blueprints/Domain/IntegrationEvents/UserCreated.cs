using System;

namespace Domain.IntegrationEvents
{
    public class UserCreated
    {
        public Guid Id { get; set; }

        public UserCreated() { }
    }
}