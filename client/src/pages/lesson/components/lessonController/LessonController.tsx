import './LessonController.scss'
import { LessonStatus } from 'pages/lesson/models/lessonState'
import { ReactElement } from 'react'
import { useDispatch } from 'react-redux'
import { startLesson } from 'store/lesson/reducer'

function LessonController({ lessonState }: Model): ReactElement {
  const dispatch = useDispatch()

  const onStart = () => {
    dispatch(startLesson())
  }

  return (
    <>
      {lessonState.btnStart && <button onClick={onStart}>Start</button>}
      {/* {lessonState.btnPause && <button onClick={onPause}>Przerwa</button>} */}
      {/* {lessonState.btnFinish && <button onClick={onFinish}>Koniec</button>} */}
    </>
  )
}

export default LessonController

interface Model {
  lessonState: LessonStatus
}
