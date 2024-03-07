import useLocalStorage from './useLocalStorage'

const LOCAL_SETTIGNS_KEY = 'LOCAL_SETTIGNS'

export const useLocalSettingsStorage = () => {
  const { getItem, setItem } = useLocalStorage(LOCAL_SETTIGNS_KEY)

  const init = () => {
    const currentValue = getItem()
    if (currentValue !== undefined) {
      return
    }

    setItem(initialLocalUserSettings)
  }

  const get = (): LocalUserSettings | undefined => {
    const storageItem = getItem() as any
    return {
      paginationPageSize: parseInt(storageItem.paginationPageSize)
    }
  }

  const update = (field: UpdateType, value: unknown) => {
    const currentValue = get() as any
    if (currentValue === undefined) {
      return
    }
    currentValue[field] = value

    setItem(currentValue)
  }

  return { init, get, update }
}

export type LocalUserSettings = {
  paginationPageSize: number
}

const initialLocalUserSettings: LocalUserSettings = {
  paginationPageSize: 20
}

export type UpdateType = 'paginationPageSize'
