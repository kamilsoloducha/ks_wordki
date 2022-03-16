import http from "common/services/http/http";
import { GroupSummary } from "../models/groupSummary";
import { SearchGroupsRequest } from "../models/requests/searchGroupsRequest";

export async function searchCards(request: SearchGroupsRequest): Promise<GroupSummary[] | any> {
  try {
    const response = await http.put<GroupSummary[]>(`/groups/search`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function searchCardsCount(request: SearchGroupsRequest): Promise<number | any> {
  try {
    const response = await http.put<number>(`/groups/search/count`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}
