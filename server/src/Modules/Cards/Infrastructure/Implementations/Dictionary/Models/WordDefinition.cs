using System.Collections.Generic;

namespace Cards.Infrastructure.Implementations.Dictionary.Models;

internal class WordDefinition
{
    public string Definition { get; set; }
    public string Example { get; set; }
    public IReadOnlyList<string> Synonyms { get; set; }
    public IReadOnlyList<string> Antonyms { get; set; }
}