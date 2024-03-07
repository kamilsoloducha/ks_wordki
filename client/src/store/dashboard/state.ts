import { ForecastModel } from 'common/models/forecastModel'

export default interface DashboardState {
  isLoading: boolean
  dailyRepeats: number
  groupsCount: number
  cardsCount: number

  forecast: ForecastModel[]
  isForecastLoading: boolean
}

export const initailState: DashboardState = {
  isLoading: true,
  dailyRepeats: 0,
  groupsCount: 0,
  cardsCount: 0,
  forecast: [],
  isForecastLoading: false
}
