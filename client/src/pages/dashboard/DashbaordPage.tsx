import { ReactElement } from 'react'
import { useNavigate } from 'react-router-dom'
import { Info } from './components/Info'
import { useDispatch, useSelector } from 'react-redux'
import { getDashboardSummary } from 'store/dashboard/reducer'
import { selectData } from 'store/dashboard/selectors'
import LoadingSpinner from 'common/components/loadingSpinner/LoadingSpinner'
import { Forecast } from './components/Forecast'
import { useTitle } from 'common/index'
import { useEffectOnce } from 'common/hooks/useEffectOnce'
import { getLanguages } from 'store/lesson/reducer'

export default function DashboardPage(): ReactElement {
  useTitle('Wordki - Dashboard')
  const data = useSelector(selectData)
  const dispatch = useDispatch()
  const navigate = useNavigate()
  useEffectOnce(() => {
    dispatch(getDashboardSummary())
    dispatch(getLanguages())
  }, [dispatch])

  if (data.isLoading) {
    return <LoadingSpinner />
  }

  return (
    <>
      <div id="info-group" className="my-3 flex gap-10 flex-wrap">
        <div className="flex-1">
          <Info
            title="Repeats"
            value={data.dailyRepeats}
            onClick={() => navigate('/lesson-settings')}
          />
        </div>
        <div className="flex-1">
          <Info title="Groups" value={data.groupsCount} onClick={() => navigate('/groups')} />
        </div>
        <div className="flex-1">
          <Info title="Cards" value={data.cardsCount} onClick={() => navigate('/cards')} />
        </div>
      </div>
      <Forecast />
    </>
  )
}
