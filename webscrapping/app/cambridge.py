import requests
from bs4 import BeautifulSoup


def cambridge(searching_term: str):

    headers = {"User-Agent": "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:108.0) Gecko/20100101 Firefox/108.0"}
    search_elements = searching_term.split(' ')
    host: str = 'https://dictionary.cambridge.org/dictionary/english/'
    query = '-'.join(search_elements)
    path: str = host + query

    response = requests.get(path, headers=headers)
    site = response.content

    soup = BeautifulSoup(site, 'html.parser')
    entities = soup.select('.def-block')
    result = []

    for entity in entities:
        definition_element = entity.select_one('.def')
        if definition_element == None:
            continue;
        definition = definition_element.get_text()
        examples = []
        example_elements = entity.select('.examp')
        for example_element in example_elements:
            example = example_element.get_text()
            examples.append(example.strip())
        
        result.append({
            "definition": definition,
            "examples": examples
        })
    
    return result
