using System.Collections.Generic;

namespace Cards.Application.Services;

public record DictionaryResponse(IEnumerable<Translation> Translations);