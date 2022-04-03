import { ForecastModel } from "pages/dashboard/models/forecastModel";
import * as queries from "../queries";
import * as responses from "../responses";
import http from "./httpBase";

export async function getDashboardSummaryApi(
  userId: string
): Promise<responses.DashboardSummaryResponse> {
  const response = await http.get<responses.DashboardSummaryResponse>(
    `dashboard/summary/${userId}`
  );
  return response as any;
}

export async function getForecast(request: queries.ForecastQuery): Promise<ForecastModel[]> {
  const response = await http.get<ForecastModel[]>("dashboard/forecast", { params: request });
  return response.data;
}
