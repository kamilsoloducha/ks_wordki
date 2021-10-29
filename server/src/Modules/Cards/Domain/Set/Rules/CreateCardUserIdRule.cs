
using System;

namespace Cards.Domain
{
    public class CreateCardUserIdRule : GuidHaveToBeDefined
    {
        public CreateCardUserIdRule(Guid userId) : base(userId, "UserId") { }
    }
}