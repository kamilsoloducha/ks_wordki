import http from "common/services/http/http";
import DashboardSummaryResponse from "../models/dashboardSummaryResponse";

export default async function dashbaord(): Promise<DashboardSummaryResponse> {
  const response = await http.get<DashboardSummaryResponse>("/users/login");
  return response.data;
}
