using System;
using System.Text.Json.Serialization;

namespace Cards.Application.Queries.Models;

public class GroupToLesson
{
    [JsonIgnore]
    public Guid OwnerId { get; set; }
    public long Id { get; set; }
    public string Name { get; set; }
    public int Front { get; set; }
    public int Back { get; set; }
    public int FrontCount { get; set; }
    public int BackCount { get; set; }
}