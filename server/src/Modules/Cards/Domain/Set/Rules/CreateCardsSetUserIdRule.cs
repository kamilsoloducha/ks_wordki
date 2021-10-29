
using System;

namespace Cards.Domain
{
    public class CreateCardsSetUserIdRule : GuidHaveToBeDefined
    {
        public CreateCardsSetUserIdRule(Guid userId) : base(userId, "UserId") { }
    }
}