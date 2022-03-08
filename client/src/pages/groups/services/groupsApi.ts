import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import AddGroupRequest from "../models/addGroupRequest";
import ConnectGroupsRequest from "../models/connectGroupsRequest";
import GroupsSummaryResponse from "../models/groupsSummaryResponse";
import { GroupSummary } from "../models/groupSummary";
import { SearchGroupRequest } from "../models/searchGroupRequest";
import UpdateGroupRequest from "../models/updateGroupRequest";

export async function groups(userId: string) {
  const response = await http.get<GroupsSummaryResponse>(`/groups/${userId}`);
  return { data: response.data };
}

export async function addGroup(request: AddGroupRequest) {
  const response = await http.post<ApiResponse<string>>("groups/add", request);
  return { data: response.data };
}

export async function updateGroup(request: UpdateGroupRequest) {
  const response = await http.put<ApiResponse<any>>("groups/update", request);
  return { data: response.data };
}

export async function connectGroups(request: ConnectGroupsRequest) {
  const response = await http.put<ApiResponse<any>>("groups/merge", request);
  return { data: response.data };
}

export async function searchGroup(request: SearchGroupRequest): Promise<GroupSummary[]> {
  try {
    const response = await http.get<GroupSummary[]>("/groups/search", { params: request });
    return response.data;
  } catch (e: any) {
    return e.response.data as any;
  }
}
