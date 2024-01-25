using System.Collections.Generic;

namespace Cards.Application.Abstraction.Dictionaries;

public class Translation
{
    public string Definition { get; set; }
    public IReadOnlyList<string> Examples { get; set; }
}