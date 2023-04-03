using System;

namespace Cards.Application.Queries.Models;

public class CardSummary
{
    public Guid UserId { get; set; }
    public long GroupId { get; set; }
    public long CardId { get; set; }
    public string GroupName { get; set; }
    public string Front { get; set; }
    public string FrontValue { get; set; }
    public string FrontExample { get; set; }
    public int FrontDrawer { get; set; }
    public bool FrontIsQuestion { get; set; }
    public bool FrontIsTicked { get; set; }
    public string Back { get; set; }
    public string BackValue { get; set; }
    public string BackExample { get; set; }
    public int BackDrawer { get; set; }
    public bool BackIsQuestion { get; set; }
    public bool BackIsTicked { get; set; }
}