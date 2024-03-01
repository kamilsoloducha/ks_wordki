import { ReactElement } from 'react'
import { TabViewItemHeader } from './TabViewItemHeader'

export function TabView({ selectedValue, items, onItemChanged }: TabViewProps): ReactElement {
  const content = items.find((x) => x.value === selectedValue)?.element

  const onClick = (value: number) => {
    if (onItemChanged) onItemChanged(value)
  }

  return (
    <>
      <div className="flex border-b border-accent-super-light">
        {items.map((item, index) => (
          <TabViewItemHeader
            key={index}
            header={item.header}
            isSelected={selectedValue === item.value}
            value={index}
            onClick={onClick}
          />
        ))}
      </div>
      <div className="w-full">{content}</div>
    </>
  )
}

export interface TabViewItemModel {
  header: string
  element: ReactElement
  value: number
}

export type TabViewProps = {
  selectedValue: number
  items: TabViewItemModel[]
  onItemChanged?: (value: number) => void
}
