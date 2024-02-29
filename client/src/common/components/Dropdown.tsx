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
  const [inputValue, setInputValue] = useState(findInitialValue(items, selectedItem, selectedIndex))
  const wrapperRef = useRef<HTMLDivElement | null>(null)
  useOutsideClickDetector(wrapperRef, () => setIsOpen(false))

  const itemClick = (item: string) => {
    setInputValue(item)
    setIsOpen(false)
    onChange && onChange(item)
  }

  const onInputChange = (value: string) => {
    setInputValue(value)
    onChange && onChange(value)
  }

  return (
    <div className="w-full relative">
      <div className="w-full flex">
        <input
          className="w-max"
          value={inputValue}
          placeholder={placeholder}
          onChange={(e) => onInputChange(e.target.value)}
        />
        <button className="w-min" onClick={() => setIsOpen(!isOpen)}>
          open
        </button>
      </div>
      {isOpen && (
        <div data-testid="drop-down-panel" ref={wrapperRef} className="w-full absolute left-0">
          {items.map((value, index) => {
            return (
              <div
                onClick={() => itemClick(value)}
                className="w-full bg-slate-900 hover:bg-slate-800 text-white"
                key={index}
              >
                {value}
              </div>
            )
          })}
        </div>
      )}
    </div>
  )
}

type DropdownProps = {
  items?: string[]
  placeholder?: string
  selectedItem?: string
  selectedIndex?: number
  onChange?: (value: string) => void
}

const findInitialValue = (
  items?: string[],
  selectedItem?: string,
  selectedIndex?: number
): string => {
  if (!items || items.length === 0) {
    return ''
  }

  if (selectedItem) {
    return selectedItem
  }

  if (selectedIndex && items[selectedIndex]) {
    return items[selectedIndex]
  }

  return ''
}
