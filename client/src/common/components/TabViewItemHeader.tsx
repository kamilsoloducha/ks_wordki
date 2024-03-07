import { ReactElement } from 'react'

export function TabViewItemHeader({
  header,
  isSelected,
  value,
  onClick
}: TabViewItemHeaderProps): ReactElement {
  return (
    <div
      className={`+ cursor-pointer p-5 font-extrabold hover:bg-accent-light ${
        isSelected ? 'border-b-2 text-accent-super-light' : ''
      }`}
      onClick={() => onClick(value)}
    >
      {header}
    </div>
  )
}

export type TabViewItemHeaderProps = {
  header: string
  isSelected: boolean
  value: number
  onClick: (value: number) => void
}
