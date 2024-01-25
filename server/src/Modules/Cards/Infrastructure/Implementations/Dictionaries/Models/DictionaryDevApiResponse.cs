using System.Collections.Generic;

namespace Cards.Infrastructure.Implementations.Dictionaries.Models;

internal class DictionaryDevApiResponse
{
    public string Word { get; set; }
    public string Phonetic { get; set; }
    public IReadOnlyList<Meaning> Meanings { get; set; }
}