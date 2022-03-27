import http from "common/services/http/http";
import { CardSummary } from "../models/cardSummary";
import { GroupSummary } from "../models/groupSummary";
import { SaveGroupRequest } from "../models/requests/saveGroupRequest";
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

export async function getCards(groupId: string): Promise<CardSummary[] | any> {
  try {
    const response = await http.get<CardSummary[]>(`/cards/${groupId}`);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function saveGroup(request: SaveGroupRequest): Promise<number | any> {
  try {
    const response = await http.post<number>(`/groups/append`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}
