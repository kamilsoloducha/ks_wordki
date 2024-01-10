import { AxiosResponse } from "axios";
import { ForecastModel } from "pages/dashboard/models/forecastModel";
import * as queries from "../queries";
import * as responses from "../responses";
import http from "./httpBase";

export async function getDashboardSummaryApi(): Promise<
  responses.DashboardSummaryResponse | ErrorResponse
> {
  const response = await http.get<responses.DashboardSummaryResponse>("dashboard/summary");
  return response.data;
}

export async function getForecast(request: queries.ForecastQuery): Promise<ForecastModel[]> {
  const response = await http.get<ForecastModel[]>("dashboard/forecast", { params: request });
  return response.data;
}

export function handleResponse<T>(axiosResponse: AxiosResponse<T>): T | ErrorResponse {
  const response = axiosResponse.data as any;
  return axiosResponse.status >= 200 && axiosResponse.status < 300
    ? response
    : ({ message: response.message, error: true } as ErrorResponse);
}

export interface ErrorResponse {
  error: boolean;
  message: string;
}
