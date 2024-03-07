import { ReactElement, useCallback } from 'react'
import './CountSelector.scss'

export function CountSelector({ selected, all, onSelectedChanged }: Model): ReactElement {
  const onChanged = useCallback(
    (event$: any) => {
      const newValue = parseInt(event$.target.value, undefined)
      // if (newValue > all) {
      //   newValue = all;
      // }
      // if (newValue < 0) {
      //   newValue = 0;
      // }
      onSelectedChanged(newValue)
    },
    [onSelectedChanged]
  )

  const change = useCallback(
    (current: number, difference: number) => {
      let newValue = current + difference
      if (newValue > all) {
        newValue = all
      }
      if (newValue < 0) {
        newValue = 0
      }
      onSelectedChanged(newValue)
    },
    [onSelectedChanged, all]
  )

  return (
    <div className="count-container">
      <p>
        Cards in lesson (<strong>{all}</strong> available):
      </p>
      <div className="count-controls">
        <button onClick={() => change(selected, -10)}>-10</button>
        <input type="number" value={selected} onChange={onChanged} />
        <button onClick={() => change(selected, 10)}>+10</button>
        <button onClick={() => change(selected, 50)}>+50</button>
        <button onClick={() => change(selected, all)}>All</button>
      </div>
    </div>
  )
}

interface Model {
  selected: number
  all: number
  onSelectedChanged: (value: number) => void
}
