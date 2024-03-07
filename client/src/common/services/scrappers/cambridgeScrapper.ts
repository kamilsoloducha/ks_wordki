import axios from 'axios'
import * as cheerio from 'cheerio'

const translateInCambridge = async (term: string): Promise<Translation> => {
  const definitions: Definition[] = []
  term = term.replaceAll(' ', '-')

  const dictionaryUrl = `https://dictionary.cambridge.org/pl/dictionary/english/${term}`

  try {
    const response = await axios.get(dictionaryUrl)
    const selector = cheerio.load(response.data)

    const dictionaryEntities = selector('.def-block')

    dictionaryEntities.each((_, entityEl) => {
      const meanings = selector(entityEl).find('.def')
      const examples: string[] = []
      let definition = ''
      meanings.each((_, meaningEl) => {
        definition = selector(meaningEl).text()
      })

      selector(entityEl)
        .find('.examp')
        .each((_, exampEl) => {
          const example = selector(exampEl).text()
          examples.push(example.trim())
        })
      definitions.push({ definition, examples })
    })
  } catch (error: unknown) {
    return { definitions: [], dictionaryUrl }
  }
  return { definitions, dictionaryUrl }
}

export type Translation = {
  dictionaryUrl: string
  definitions: Definition[]
}

export type Definition = {
  definition: string
  examples: string[]
}

export { translateInCambridge }
