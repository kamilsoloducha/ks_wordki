import * as key from '../models/keyCodes'
import * as resultTypes from '../models/resultTypes'
import * as actions from 'store/lesson/reducer'
import * as sel from 'store/lesson/selectors'
import React, { ReactElement, useCallback, useEffect, useMemo, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import Answer from './Answer'
import Question from './Question'

export default function Fiszka(): ReactElement {
  const dispatch = useDispatch()
  const status = useSelector(sel.selectLessonState)
  const currectRepeat = useSelector(sel.selectCurrectRepeat)
  const [clickPosition, setClickPosition] = useState<number | undefined>(undefined)
  const [clickAndDragDelata, setClickAndDragDelta] = useState<number | undefined>(undefined)
  const [clickTimestamp, setClickTimestamp] = useState<number>(0)

  const questionElement = useMemo(
    () =>
      currectRepeat && (
        <Question
          value={currectRepeat.question}
          example={currectRepeat.questionExample}
          language={1}
        />
      ),
    [currectRepeat]
  )

  const answerElement = useMemo(
    () =>
      currectRepeat && (
        <Answer
          isVisible={status.answer}
          correctAnswer={currectRepeat.answer}
          exampleAnswer={currectRepeat.answerExample}
          userAnswer={currectRepeat.answer}
        />
      ),
    [currectRepeat, status]
  )

  const handleEventEvent = useCallback(
    (event: KeyboardEvent) => {
      switch (event.key) {
        case key.Enter: {
          dispatch(actions.check())
          break
        }
        case key.Left: {
          dispatch(actions.wrong({ result: resultTypes.Wrong }))
          break
        }
        case key.Right: {
          dispatch(actions.correct({ result: resultTypes.Correct }))
          break
        }
      }
    },
    [dispatch]
  )

  useEffect(() => {
    document.addEventListener('keydown', handleEventEvent)
    return () => {
      document.removeEventListener('keydown', handleEventEvent)
    }
  }, [handleEventEvent])

  if (!status.card || !currectRepeat) {
    return <></>
  }

  const onUntouch = () => {
    let percentage = clickAndDragDelata
      ? Math.abs(clickAndDragDelata / document.body.clientWidth)
      : 0
    percentage = percentage > 1 ? 1 : percentage

    if (percentage > 0.4) {
      if (clickAndDragDelata! > 0) {
        dispatch(actions.correct({ result: resultTypes.Correct }))
      } else {
        dispatch(actions.wrong({ result: resultTypes.Wrong }))
      }
    }

    setClickAndDragDelta(undefined)
    setClickPosition(undefined)
  }

  const onFingerMove = (event: React.TouchEvent<HTMLDivElement>) => {
    if (!clickPosition) {
      return
    }

    if (status.btnCheck) {
      return
    }

    setClickAndDragDelta(event.touches[0].clientX - clickPosition)
  }

  const onTouch = (event: React.TouchEvent<HTMLDivElement>) => {
    setClickPosition(event.touches[0].clientX)

    if (event.timeStamp - clickTimestamp < 300) {
      if (status.btnCheck) {
        dispatch(actions.check())
      }
    }
    setClickTimestamp(event.timeStamp)
  }

  let percentage = clickAndDragDelata ? Math.abs(clickAndDragDelata / document.body.clientWidth) : 0
  percentage = percentage > 1 ? 1 : percentage

  return (
    <div
      className="relative h-max"
      onTouchStart={onTouch}
      onTouchEnd={onUntouch}
      onTouchMove={onFingerMove}
    >
      {clickAndDragDelata && clickAndDragDelata < 0 && (
        <div
          className="absolute z-10 size-full bg-gradient-to-r from-red-500"
          style={{ opacity: percentage }}
        ></div>
      )}
      {clickAndDragDelata && clickAndDragDelata > 0 && (
        <div
          className="absolute z-10 size-full bg-gradient-to-l from-green-500"
          style={{ opacity: percentage }}
        ></div>
      )}
      {questionElement}
      <hr />
      {answerElement}
    </div>
  )
}
