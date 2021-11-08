using System;

namespace Domain.IntegrationEvents
{
    public class AnswerRegistered
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public Guid CardId { get; set; }
        public int Side { get; set; }
        public int Result { get; set; }
    }
}