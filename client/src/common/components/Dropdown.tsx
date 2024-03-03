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
    <div className="w-full relative">
      <div className="w-full h-max relative items-center flex p-3 text-l rounded-md bg-accent-dark text-accent-super-light border-2 border-zinc-600">
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
            className="w-6 h-6"
          >
            <path strokeLinecap="round" strokeLinejoin="round" d="m19.5 8.25-7.5 7.5-7.5-7.5" />
          </svg>
        </button>
      </div>
      {isOpen && (
        <div
          data-testid="drop-down-panel"
          ref={wrapperRef}
          className="w-full absolute left-0 border-2 border-accent-light  rounded-md"
        >
          {items.map((value, index) => {
            return (
              <div
                onClick={() => itemClick(value)}
                className="w-full bg-accent-dark text-accent-super-light mt-[2px] text-l border-b border-accent-light cursor-pointer ps-2 hover:bg-lighter-a-bit"
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
