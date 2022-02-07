import http from "common/services/http/http";
import DashboardSummaryResponse from "../models/dashboardSummaryResponse";

export async function getDashboardSummaryApi(userId: string): Promise<DashboardSummaryResponse> {
  const response = await http.get<DashboardSummaryResponse>(`cards/dashboard/summary/${userId}`);
  return response as any;
}
