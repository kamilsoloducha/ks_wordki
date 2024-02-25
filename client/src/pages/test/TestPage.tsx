import './TestPage.scss'
import { useEffectOnce } from 'common/hooks/useEffectOnce'
import { Definition, translateInCambridge } from 'common/services/scrappers/cambridgeScrapper'
import { Translation, translateInDiki } from 'common/services/scrappers/dikiScrapper'
import React from 'react'
import { KeyboardEvent, ReactElement, useEffect, useState } from 'react'
import { NavLink, useSearchParams } from 'react-router-dom'
import LoadingSpinner from 'common/components/loadingSpinner/LoadingSpinner'
import { summaries, addCardRequest } from 'api/index'
import { Dropdown } from 'primereact/dropdown'

export default function TestPage(): ReactElement {
  const [searchParams, setSearchParams] = useSearchParams({ query: '', dic: 'Diki' })
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [selectedDefinitions, setSelectedDefinitions] = useState<Definition[]>([])
  const [definition, setDefinition] = useState<string>('')
  const [groups, setGroups] = useState<Group[]>([])
  const [selectedGroup, setSelectedGroup] = useState<Group | null>(null)
  const [query, setQuery] = useState(searchParams.get('query') ?? '')
  const [dictionary, setDictionary] = useState(searchParams.get('dic') ?? '')
  const [transaltion, setTranslation] = useState<Translation>({
    definitions: [],
    dictionaryUrl: ''
  })
  let searchingFunction: (term: string) => Promise<Translation>
  switch (dictionary) {
    case 'Diki':
      searchingFunction = translateInDiki
      break
    case 'Cambridge':
      searchingFunction = translateInCambridge
      break
    default:
      break
  }

  useEffect(() => {
    setSearchParams((prev) => {
      prev.set('dic', dictionary)
      return prev
    })
    switch (dictionary) {
      case 'Diki':
        searchingFunction = translateInDiki
        break
      case 'Cambridge':
        searchingFunction = translateInCambridge
        break
      default:
        break
    }
    if (query.length > 0) {
      setIsLoading(true)
      searchingFunction(query)
        .then((answer) => setTranslation(answer))
        .then((_) => setIsLoading(false))
        .catch((_) => setIsLoading(false))
    }
  }, [dictionary])

  useEffectOnce(() => {
    if (query.length > 0) {
      searchingFunction(query)
        .then((answer) => setTranslation(answer))
        .then((_) => setIsLoading(false))
        .catch((_) => setIsLoading(false))
    }

    summaries().then((response) => {
      const groups: Group[] = response.data.map((group) => {
        return { id: group.id, name: group.name, front: group.front, back: group.back } as Group
      })
      setGroups(groups)
    })
  }, [query])

  const onClick = async () => {
    setSearchParams((prev) => {
      prev.set('query', query)
      return prev
    })
    if (!query) {
      setTranslation({
        definitions: [],
        dictionaryUrl: ''
      })
      return
    }
    setIsLoading(true)
    searchingFunction(query)
      .then((answer) => setTranslation(answer))
      .then((_) => setIsLoading(false))
      .catch((_) => setIsLoading(false))
  }

  const onKeyDown = async (event: KeyboardEvent<Element>) => {
    if (event.key === 'Enter') {
      await onClick()
    }
  }

  const translationClicked = (newDefinition: Definition) => {
    if (!selectedDefinitions.includes(newDefinition)) {
      setSelectedDefinitions([...selectedDefinitions, newDefinition])
      setDefinition(definition + ' ' + newDefinition.definition.replace(/\s{2,}/g, ' ').trim())
    }
  }

  const addCardClicked = async () => {
    if (!selectedGroup) {
      return
    }
    const groupId = selectedGroup?.id

    const result = await addCardRequest(groupId, {
      comment: '',
      front: { value: query, example: '', isUsed: false },
      back: { value: definition, example: '', isUsed: false }
    })
    if (result) {
      setDefinition('')
      setSelectedDefinitions([])
    }
  }

  return (
    <>
      <input
        onChange={(event) => setQuery(event.target.value)}
        value={query}
        onKeyDown={onKeyDown}
      />
      <button onClick={onClick}>Check it</button>
      <button onClick={() => setDictionary('Diki')}>Diki</button>
      <button onClick={() => setDictionary('Cambridge')}>Cambridge</button>
      <Dropdown
        value={selectedGroup}
        options={groups}
        onChange={(event) => setSelectedGroup(event.value)}
        itemTemplate={dropdownItemLayout}
        valueTemplate={dropdownItemLayout}
        optionLabel="name"
        placeholder="Select group..."
      />
      <p>
        {query} -
        <input
          className="definition-input"
          value={definition}
          onChange={(event) => setDefinition(event.target.value)}
        />
        <button disabled={selectedGroup == null} onClick={addCardClicked}>
          Add Card
        </button>
      </p>
      {isLoading ? (
        <LoadingSpinner></LoadingSpinner>
      ) : (
        <>
          {transaltion.dictionaryUrl.length > 0 ? (
            <p>
              Dictinary address:{' '}
              <NavLink to={transaltion.dictionaryUrl}>{transaltion.dictionaryUrl}</NavLink>
            </p>
          ) : (
            <></>
          )}

          {transaltion.definitions.length > 0 ? (
            <>
              <h3>Translations:</h3>
              {transaltion.definitions.map((def, id) => {
                return (
                  <React.Fragment key={id}>
                    <div className="translation-item" onClick={() => translationClicked(def)}>
                      <b className="definition">{def.definition}</b>
                      {def.examples.map((example, id) => {
                        return <p key={id}>- {example}</p>
                      })}
                    </div>
                  </React.Fragment>
                )
              })}
            </>
          ) : (
            <></>
          )}
        </>
      )}
    </>
  )
}

type Group = {
  id: string
  name: string
  front: string
  back: string
}

const dropdownItemLayout = (option: Group, props: any = null) => {
  if (option) {
    return (
      <div className="group-item">
        <strong>{option.name}</strong>
      </div>
    )
  }
  return (
    <>
      <span>{props.placeholder}</span>
    </>
  )
}
