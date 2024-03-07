import './LessonTypeSelector.scss'
import { ReactElement } from 'react'

export function LessonTypeSelector({ selected, onSelectedChanged }: Model): ReactElement {
  const onTypeChanged = ($event: any) => {
    const value = parseInt($event.target.value)
    onSelectedChanged(value)
  }

  return (
    <div className="lesson-type-container">
      <p>Lesson type:</p>
      <div className="lesson-type-items-container">
        <input
          className="input-fiszki"
          type="radio"
          name="lessonType"
          id="type1"
          value={1}
          onChange={onTypeChanged}
          checked={selected === 1}
        />
        <label htmlFor="type1" className="item-container">
          <img alt="cards" className="setting-icon" src="/svgs/cards.svg" />
          <p>Fiszki</p>
        </label>
        <input
          className="input-fiszki"
          type="radio"
          name="lessonType"
          id="type2"
          value={2}
          onChange={onTypeChanged}
          checked={selected === 2}
        />
        <label htmlFor="type2" className="item-container">
          <img alt="typing" className="setting-icon" src="/svgs/keyboard.svg" />
          <p>Typing</p>
        </label>
      </div>
    </div>
  )
}

interface Model {
  selected: number
  onSelectedChanged: (value: number) => void
}
