using System;

namespace Domain.IntegrationEvents;

public class AnswerRegistered
{
    public Guid UserId { get; set; }
    public long CardId { get; set; }
    public int SideType { get; set; }
    public int Result { get; set; }
}