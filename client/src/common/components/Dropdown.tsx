import { selectItem } from '@/src/store/groups/reducer'
import { val } from 'cheerio/lib/api/attributes'
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
    <div className="w-full h-full relative">
      <div className="w-full h-full relative">
        <input
          className="w-full right-0 bg-accent-dark text-accent-super-light"
          value={inputValue}
          placeholder={placeholder}
          onChange={(e) => onInputChange(e.target.value)}
        />
        <button className="w-min absolute right-0" onClick={() => setIsOpen(!isOpen)}>
          v
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
