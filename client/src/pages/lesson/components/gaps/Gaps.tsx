import { ReactElement, useEffect, useState } from 'react'
import Question from '../Question'
import * as select from 'store/lesson/selectors'
import { useDispatch, useSelector } from 'react-redux'

export function Gaps(): ReactElement {
  const [answerMap, setAnswerMap] = useState<[string, number][]>([])
  const repeat = useSelector(select.selectCurrectRepeat)

  useEffect(() => {
    const answerMap = splitToChars(repeat.answer.toLowerCase())
    setAnswerMap(Array.from(answerMap))
  }, [repeat])

  return (
    <>
      <Question value={repeat.question} example={repeat.questionExample} language={1} />
      {answerMap.map((value, index) => {
        return (
          <div key={value[0]}>
            {value[0]} - {value[1]}
          </div>
        )
      })}
    </>
  )
}

export function splitToChars(text: string): Map<string, number> {
  const result = new Map<string, number>()

  for (let i = 0; i < text.length; i++) {
    const currentChar = text.charAt(i)
    if (!currentChar.match(/[a-zA-Z0-9]/i)) {
      continue
    }
    const currentValue = result.get(currentChar)
    result.set(currentChar, currentValue ? currentValue + 1 : 1)
  }

  return result
}
