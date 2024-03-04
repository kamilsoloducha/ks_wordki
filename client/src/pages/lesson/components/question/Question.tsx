import './Question.scss'
import { ReactElement } from 'react'

export default function Question({ value, language, example }: Model): ReactElement {
  return (
    <div className="question-main-container">
      <div className="question-value">{value}</div>
      <div className="question-example">{example}</div>
    </div>
  )
}

interface Model {
  value: string
  language: number
  example?: string
}
