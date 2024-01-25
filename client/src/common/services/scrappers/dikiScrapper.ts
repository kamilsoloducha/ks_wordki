import axios from "axios";
import * as cheerio from "cheerio";

const translateInDiki = async (term: string): Promise<Translation> => {
  const definitions: Definition[] = [];
  term = term.replaceAll(" ", "+");

  const dictionaryUrl = `https://www.diki.pl/slownik-angielskiego?q=${term}`;

  try {
    const response = await axios.get(dictionaryUrl);
    const selector = cheerio.load(response.data);

    const dictionaryEntities = selector(
      ".diki-results-container .diki-results-left-column .dictionaryEntity"
    );

    dictionaryEntities.each((_, entityEl) => {
      const meanings = selector(entityEl).children(".foreignToNativeMeanings");
      meanings.each((_, meaningEl) => {
        const definitionsEl = selector(meaningEl).children("li");
        definitionsEl.each((_, defEl) => {
          const defs: string[] = [];
          const examples: string[] = [];
          selector(defEl)
            .children("span")
            .each((_, span) => {
              defs.push(selector(span).text());
            });

          selector(defEl)
            .children(".exampleSentence")
            .each((_, exampleEl) => {
              examples.push(selector(exampleEl).text().trim());
            });
          definitions.push({ definition: defs.join(", "), examples });
        });
      });
    });
  } catch (error: unknown) {
    return { definitions: [], dictionaryUrl };
  }

  return { definitions, dictionaryUrl };
};

export type Translation = {
  dictionaryUrl: string;
  definitions: Definition[];
};

export type Definition = {
  definition: string;
  examples: string[];
};

export { translateInDiki };
