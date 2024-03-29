import './LessonResult.scss'
import * as actions from 'store/lesson/reducer'
import { ReactElement, useCallback, useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { selectLessonHistory, selectResults, selectSettings } from 'store/lesson/selectors'
import { History } from 'pages/lesson/components/history/History'
import UserRepeat from 'pages/lesson/models/userRepeat'
import CardDialog from 'common/components/CardDialog'
import { CardFormModel } from 'common/components/CardForm'
import { useTitle } from 'common/index'

export default function LessonResult(): ReactElement {
  useTitle('Wordki - Results')
  const dispatch = useDispatch()
  const history = useNavigate()
  const results = useSelector(selectResults)
  const lessonHistory = useSelector(selectLessonHistory)
  const lessonSettings = useSelector(selectSettings)

  const [userRepeats, setUserRepeats] = useState<UserRepeat[]>(lessonHistory)
  const [selectedItem, setSelectedItem] = useState<UserRepeat | undefined>(undefined)

  const userAnswerColumn = userAnswerColumnNecessary(lessonSettings.type)

  useEffect(() => {
    dispatch(actions.resetLesson())
  }, [dispatch])

  const onContinue = useCallback(() => {
    dispatch(actions.getCards({ navigate: history }))
  }, [dispatch])

  const startNew = useCallback(() => {
    history('/lesson-settings')
  }, [history])

  const finish = useCallback(() => {
    history('/dashboard')
  }, [history])

  const showCorrect = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, 1))
  }

  const showAccepted = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, 0))
  }

  const showWrong = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, -1))
  }

  const showAll = () => {
    setUserRepeats(lessonHistory)
  }

  const onSubmit = (form: CardFormModel) => {
    if (!selectedItem) return
    form.backEnabled = null
    form.frontEnabled = null
    dispatch(actions.updateCard({ form, groupId: '' }))
    setSelectedItem(undefined)
  }

  const onDelete = (form: CardFormModel) => undefined

  return (
    <>
      <div className="lesson-results-container">
        <div className="buttons">
          <button onClick={onContinue}>Continue</button>
          <button onClick={startNew}>Start New</button>
          <button onClick={finish}>Finish</button>
        </div>
        <div className="details">
          <div className="overview">
            <b>Results:</b>
            <p onClick={showCorrect}>Correct: {results.correct}</p>
            <p onClick={showAccepted}>Accepeted: {results.accept}</p>
            <p onClick={showWrong}>Wrong: {results.wrong}</p>
            <p onClick={showAll}>Answers: {results.answers}</p>
          </div>
          <div className="table">
            <History
              history={userRepeats}
              userAnswer={userAnswerColumn}
              onItemClick={(item) => setSelectedItem(item)}
            />
          </div>
        </div>
      </div>
      <CardDialog
        card={getFormModelFromUserRepeat(selectedItem)}
        onHide={() => setSelectedItem(undefined)}
        onSubmit={onSubmit}
        onDelete={onDelete}
      />
    </>
  )
}

export function userAnswerColumnNecessary(type: number): boolean {
  return type === 2
}

export function filterLessonHistory(items: UserRepeat[], result: number): UserRepeat[] {
  return items.filter((x) => x.result === result)
}

export function getFormModelFromUserRepeat(
  userRepeat: UserRepeat | undefined
): CardFormModel | undefined {
  if (!userRepeat) return undefined

  const form: CardFormModel = {
    cardId: userRepeat.repeat.cardId,
    backEnabled: false,
    comment: '',
    isTicked: false,
    frontValue: '',
    frontExample: '',
    frontEnabled: undefined,
    backValue: '',
    backExample: ''
  }

  return form
}
