import { useCallback, useEffect, useState } from 'react'
import { Dropdown } from 'common/components/Dropdown'
import { useLocalSettingsStorage } from 'common/hooks/useSettingsStorage'

export function Pagination({
  totalCount,
  search,
  pageSize = 10,
  page,
  onPageChagned,
  onSearchChanged,
  onPageSizeChanged
}: PaginationProps) {
  const [currectPage, setCurrectPage] = useState(page ?? 1)
  const totalPages = Math.ceil(totalCount / pageSize)
  const buttons = getPagesToDispaly(totalPages, currectPage)

  const changePage = useCallback(
    (page: number) => {
      if (page <= 0 || page > totalPages || page === currectPage) return
      setCurrectPage(page)
      onPageChagned({
        currectPage: page,
        count: pageSize,
        first: (page - 1) * pageSize + 1
      })
    },
    [totalPages, currectPage, pageSize]
  )

  const onSearchTextChanged = useCallback(
    (event: any) => {
      const text = event.target.value
      if (onSearchChanged) {
        onSearchChanged(text)
      }
    },
    [onSearchChanged]
  )

  useEffect(() => {
    changePage(1)
  }, [totalCount])

  function onDropdownValueChanged<TType>(newValue: TType): void {
    if (!onPageSizeChanged) {
      return
    }
    let newSize = 20
    switch (typeof newValue) {
      case 'number':
        newSize = newValue
        break
      case 'string':
        newSize = parseInt(newValue)
        if (typeof newSize === 'number') {
          new Error(`Value '${newValue}' cannot be casted to number value`)
        }
        break
      default:
        new Error(`Value '${newValue}' cannot be casted to number value`)
        break
    }
    onPageSizeChanged(newSize)
    setCurrectPage(1)
    useLocalSettingsStorage().update('paginationPageSize', newSize)
  }

  return (
    <div className="flex justify-between relative">
      <div className="flex w-1/3 items-center">
        {onSearchChanged && (
          <input
            className="p-3 text-l rounded-md w-[125px] focus:w-[300px] transition-width bg-neutral-800 focus:bg-neutral-700 text-zinc-400 border-2 border-zinc-600"
            type="search"
            placeholder="Search"
            value={search}
            onChange={onSearchTextChanged}
          />
        )}
      </div>
      <div className="flex w-1/3 justify-center items-center">
        <div
          className={`pagination-button ${currectPage === 1 ? 'disabled' : ''}`}
          onClick={() => changePage(1)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="m18.75 4.5-7.5 7.5 7.5 7.5m-6-15L5.25 12l7.5 7.5"
            />
          </svg>
        </div>
        <div
          className={`pagination-button ${currectPage === 1 ? 'disabled' : ''}`}
          onClick={() => changePage(currectPage - 1)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 19.5 8.25 12l7.5-7.5" />
          </svg>
        </div>
        {buttons.map((x) => {
          return (
            <div
              className={`pagination-button ${currectPage === x ? 'selected' : ''}`}
              key={x}
              onClick={() => changePage(x)}
            >
              {x}
            </div>
          )
        })}
        <div
          className={`pagination-button ${currectPage === totalPages ? 'disabled' : ''}`}
          onClick={() => changePage(currectPage + 1)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path strokeLinecap="round" strokeLinejoin="round" d="m8.25 4.5 7.5 7.5-7.5 7.5" />
          </svg>
        </div>
        <div
          className={`pagination-button ${currectPage === totalPages ? 'disabled' : ''}`}
          onClick={() => changePage(totalPages)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="m5.25 4.5 7.5 7.5-7.5 7.5m6-15 7.5 7.5-7.5 7.5"
            />
          </svg>
        </div>
      </div>
      <div className="flex w-1/3 justify-end items-center">
        <div className="w-20 h-min">
          <Dropdown
            selectedItem={{ value: pageSize, label: pageSize.toString() }}
            items={[
              { label: '10', value: 10 },
              { label: '20', value: 20 },
              { label: '30', value: 30 }
            ]}
            onChange={(e) => onDropdownValueChanged(e.value)}
          />
        </div>
      </div>
    </div>
  )
}

export type PaginationProps = {
  totalCount: number
  pageSize?: number
  search?: string
  page?: number
  onPageChagned: (event: PageChangedEvent) => void
  onSearchChanged?: (text: string) => void
  onPageSizeChanged?: (size: number) => void
}

function getPagesToDispaly(totalPages: number, currectPage: number): number[] {
  const result: number[] = []
  const factor = 3
  for (let i = currectPage - factor; i <= currectPage + factor; i++) {
    result.push(i)
  }
  return result.filter((x) => x > 0 && x <= totalPages)
}

export type PageChangedEvent = {
  currectPage: number
  first: number
  count: number
}
