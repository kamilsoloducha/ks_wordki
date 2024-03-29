using System.Collections.Generic;

namespace Cards.Infrastructure.Implementations.Dictionaries.Models;

internal class Meaning
{
    public string PartOfSpeech { get; set; }
    public IReadOnlyList<WordDefinition> Definitions { get; set; }
}