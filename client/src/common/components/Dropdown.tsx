import useOutsideClickDetector from 'common/hooks/useOutsideClickDetector'
import { useRef } from 'react'
import { ReactNode, useState } from 'react'

export function Dropdown({
  items,
  selectedIndex,
  selectedItem,
  placeholder,
  onChange
}: DropdownProps): ReactNode {
  items = items ? items : []
  const [isOpen, setIsOpen] = useState(false)
  const [inputValue, setInputValue] = useState<string>(
    findInitialValue(items, selectedItem, selectedIndex)?.label ?? ''
  )

  const wrapperRef = useRef<HTMLDivElement | null>(null)
  useOutsideClickDetector(wrapperRef, () => setIsOpen(false))

  const itemClick = (item: TType) => {
    setInputValue(item.label)
    setIsOpen(false)
    onChange && onChange(item)
  }

  const onInputChange = (value: string) => {
    setInputValue(value)

    const selectedItem = items?.find((x) => x.label === value) ?? {
      label: value,
      value: value
    }

    onChange && onChange(selectedItem)
  }

  return (
    <div className="relative w-full">
      <div className="text-l relative flex h-max w-full items-center rounded-md border-2 border-zinc-600 bg-accent-dark p-3 text-accent-super-light">
        <input
          className="w-full bg-transparent"
          value={inputValue}
          placeholder={placeholder}
          onChange={(e) => onInputChange(e.target.value)}
        />
        <button className="hover:bg-lighter-a-bit" onClick={() => setIsOpen(!isOpen)}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            aria-hidden="true"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="size-6"
          >
            <path strokeLinecap="round" strokeLinejoin="round" d="m19.5 8.25-7.5 7.5-7.5-7.5" />
          </svg>
        </button>
      </div>
      {isOpen && (
        <div
          data-testid="drop-down-panel"
          ref={wrapperRef}
          className="absolute left-0 w-full rounded-md border-2  border-accent-light"
        >
          {items.map((value, index) => {
            return (
              <div
                onClick={() => itemClick(value)}
                className="text-l mt-[2px] w-full cursor-pointer border-b border-accent-light bg-accent-dark ps-2 text-accent-super-light hover:bg-lighter-a-bit"
                key={index}
              >
                {value.label}
              </div>
            )
          })}
        </div>
      )}
    </div>
  )
}

export type TType = {
  label: string
  value: unknown
}

type DropdownProps = {
  items?: TType[]
  placeholder?: string
  selectedItem?: TType
  selectedIndex?: number
  onChange?: (value: TType) => void
}

function findInitialValue(
  items?: TType[],
  selectedItem?: TType,
  selectedIndex?: number
): TType | undefined {
  if (!items || items.length === 0) {
    return undefined
  }

  if (selectedItem) {
    return selectedItem
  }

  if (selectedIndex !== undefined && items[selectedIndex]) {
    return items[selectedIndex]
  }

  return undefined
}
