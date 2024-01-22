import requests
from bs4 import BeautifulSoup, Tag


def diki(searching_term: str):
    search_elements = searching_term.split(' ')
    host: str = 'https://www.diki.pl/slownik-angielskiego?q='
    query = '%20'.join(search_elements)
    path: str = host + query

    response = requests.get(path)
    site = response.content

    soup = BeautifulSoup(site, 'html.parser')
    dictionary_entities = soup.select('.diki-results-container .diki-results-left-column .dictionaryEntity');

    result = []

    for dictionary_entity in dictionary_entities:
        meanings = dictionary_entity.select('.foreignToNativeMeanings li')
        for meaning in meanings:
            translations = meaning.select('span.hw')
            translations = ', '.join(map(Tag.get_text, translations))
            
            example_elements = meaning.select('.exampleSentence')
            examples= []
            for example_element in example_elements:
                example = example_element.get_text().strip()
                examples.append(example)

            result.append(
                {
                    "definition": translations,
                    "examples": examples
                }
            )
    
    return result

