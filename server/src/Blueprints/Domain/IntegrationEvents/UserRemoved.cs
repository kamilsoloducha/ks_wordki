using System;

namespace Domain.IntegrationEvents
{
    public class UserRemoved
    {
        public Guid Id { get; set; }
    }

    public class AnswerRegistered
    {
        public readonly Guid CardId;

        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public int Side { get; set; }
        public int result { get; set; }
    }
}