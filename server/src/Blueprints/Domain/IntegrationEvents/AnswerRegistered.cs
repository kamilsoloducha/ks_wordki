using System;

namespace Domain.IntegrationEvents;

public class AnswerRegistered
{
    public Guid UserId { get; set; }
    public long SideId { get; set; }
    public int Result { get; set; }
}