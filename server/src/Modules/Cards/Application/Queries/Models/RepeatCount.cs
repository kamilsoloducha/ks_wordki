using System;
using Newtonsoft.Json;

namespace Cards.Application.Queries.Models;

public class RepeatCount
{
    public int Count { get; set; }
    public DateTime Date { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}