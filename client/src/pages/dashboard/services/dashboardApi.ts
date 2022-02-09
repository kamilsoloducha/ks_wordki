import http from "common/services/http/http";
import DashboardSummaryResponse from "../models/dashboardSummaryResponse";
import { ForecastModel } from "../models/forecastModel";
import { GetForecastRequest } from "../requests/getForecastRequest";

export async function getDashboardSummaryApi(userId: string): Promise<DashboardSummaryResponse> {
  const response = await http.get<DashboardSummaryResponse>(`cards/dashboard/summary/${userId}`);
  return response as any;
}

export async function getForecast(request: GetForecastRequest): Promise<ForecastModel[]> {
  const response = await http.get<ForecastModel[]>("dashboard/forecast", { params: request });
  return response.data;
}
