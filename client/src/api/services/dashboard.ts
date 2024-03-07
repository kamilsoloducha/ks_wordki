import { AxiosResponse } from 'axios'
import { ForecastModel } from '@/src/common/models/forecastModel'
import * as q from '../queries'
import * as res from '../responses'
import http from './httpBase'

const PATH = '/dashboard'

export async function getSummary(): Promise<AxiosResponse<res.DashboardSummary>> {
  console.log('getSummary')
  return await http.get<res.DashboardSummary>(`${PATH}/summary`)
}

export async function getForecast(
  request: q.ForecastQuery
): Promise<AxiosResponse<ForecastModel[]>> {
  return await http.get<ForecastModel[]>(`${PATH}/forecast`, { params: request })
}
