using System;
using Newtonsoft.Json;

namespace Cards.Application.Queries.Models;

public record RepeatCount(int Count, DateTime Date)
{
    [JsonIgnore]
    public Guid OwnerId { get; private set; }
}