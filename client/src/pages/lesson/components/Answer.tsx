import { ReactElement } from 'react'
import * as leven from 'pages/lesson/services/levenshteinDistance'
import { compare } from 'pages/lesson/services/compare'

export default function Answer({
  isVisible,
  userAnswer,
  correctAnswer,
  exampleAnswer
}: Model): ReactElement {
  const isCorrect = compare(correctAnswer, userAnswer)
  const answer = leven.levenshtein(correctAnswer, userAnswer)
  return (
    <div className="flex h-[30vh] w-full select-none flex-col flex-wrap justify-center gap-5 text-center md:select-auto">
      <div className="correct-answer-value w-full text-center text-4xl">
        {answer.map((item: leven.LevenpathResult, i: number) => (
          <span
            key={i}
            className={`${
              item.type === leven.Equal || isCorrect ? 'text-green-500' : 'text-red-500'
            } ${item.type === leven.Insert ? 'line-through' : ''}
            ${isVisible ? '' : 'opacity-0'}`}
          >
            {item.char}
          </span>
        ))}
      </div>
      <div className={`w-full ${isVisible ? '' : 'opacity-0'}`}>{exampleAnswer}</div>
    </div>
  )
}

interface Model {
  isVisible: boolean
  userAnswer: string
  correctAnswer: string
  exampleAnswer: string
}
