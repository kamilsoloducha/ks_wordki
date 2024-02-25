import './LessonSetting.scss'
import * as act from 'store/lesson/reducer'
import { ReactElement, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectIsProcessing, selectSettings } from 'store/lesson/selectors'
import Settings from './components/repetitionSettings/RepetitionSettings'
import { TabView, TabViewItemModel } from 'common/components/tabView/TabeView'
import { LessonSettings as SettingsModel } from 'pages/lessonSettings/models/lessonSettings'
import NewCardsSettings from './components/newCardsSettings/NewCardsSettings'
import { LessonMode as mode } from './models/lesson-mode'
import LoadingSpinner from 'common/components/loadingSpinner/LoadingSpinner'
import { useTitle } from 'common/index'
import { useNavigate } from 'react-router-dom'

export default function LessonSettingsPage(): ReactElement {
  useTitle('Wordki - Lesson')
  const settings = useSelector(selectSettings)
  const isProcessing = useSelector(selectIsProcessing)
  const dispatch = useDispatch()
  const navigate = useNavigate()

  useEffect(() => {
    dispatch(act.resetResults())
    dispatch(act.getCardsCount())
    dispatch(act.getLanguages())
  }, [dispatch])

  const items: TabViewItemModel[] = [
    {
      header: 'Repetition',
      element: <Settings />,
      value: mode.Repetition
    },
    {
      header: 'New words',
      element: <NewCardsSettings />,
      value: mode.New
    }
  ]

  const onModeChanged = (value: number) => {
    dispatch(act.setSettingsMode({ mode: value + 1 }))
  }

  const onStartClick = () => {
    dispatch(act.resetResults())
    dispatch(act.getCards({ navigate }))
  }

  if (isProcessing) {
    return <LoadingSpinner />
  }

  return (
    <>
      <TabView selectedValue={settings.mode} items={items} onItemChanged={onModeChanged} />
      <div className="settings-container">
        <button disabled={!canLessonStart(settings) || isProcessing} onClick={onStartClick}>
          Start
        </button>
      </div>
    </>
  )
}

function canLessonStart(settings: SettingsModel): boolean {
  return settings.count > 0 && settings.type >= 0
}
