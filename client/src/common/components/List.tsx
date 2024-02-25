import React, { ReactNode } from 'react'

export type ListProps<TItemProps> = {
  items: TItemProps[]
  template: (props: TItemProps) => ReactNode
}

export default function List<TItemProps>({
  items,
  template
}: ListProps<TItemProps>): ReactNode {
  return (
    <>
      {items.map((item, index) => {
        return <React.Fragment key={index}>{template(item)}</React.Fragment>
      })}
    </>
  )
}
