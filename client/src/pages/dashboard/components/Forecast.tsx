import { ReactElement } from 'react'
import { useSelector } from 'react-redux'
import { selectForecast } from 'store/dashboard/selectors'

export function Forecast(): ReactElement {
  const data = useSelector(selectForecast)

  return (
    <div className="w-full rounded-2xl bg-white px-[10%] py-5">
      <div className="w-full border-b-2 border-b-slate-400 pb-3 text-center text-3xl ">
        Repetitions forecast
      </div>
      <div className="flex w-full flex-wrap gap-10 pt-5">
        {data
          ?.map((item: any) => prepareForecasModel(item))
          .map((item: any, index: number) => {
            return (
              <ForecaseItem
                key={index}
                count={item.count}
                date={item.date}
                weekDay={item.weekDay}
              />
            )
          })}
      </div>
    </div>
  )
}

function ForecaseItem({ count, weekDay, date }: ForecastItemProps): ReactElement {
  return (
    <div className="flex-1 text-center">
      <div className="text-l">
        {date}
        <br />
        {weekDay}
      </div>
      <div className="mt-4 text-xl">{count}</div>
    </div>
  )
}

interface ForecastItemProps {
  count: number
  weekDay: string
  date: string
}

function prepareForecasModel(data: any): ForecastItemProps {
  const date = new Date(data.date)
  return {
    count: data.count,
    weekDay: date.toLocaleDateString(undefined, { weekday: 'long' }),
    date: date.toLocaleDateString(undefined, { day: '2-digit', month: 'short' })
  }
}
