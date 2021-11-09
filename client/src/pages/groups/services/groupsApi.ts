import http from "common/services/http/http";
import GroupsSummaryResponse from "../models/groupsSummaryResponse";

export async function groups(userId: string) {
  const response = await http.get<GroupsSummaryResponse>(`/groups/${userId}`);
  return { data: response.data };
}
