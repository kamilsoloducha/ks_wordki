import { ApiResponse } from "common/models/response";
import { GroupSummary } from "pages/groupsSearch/models/groupSummary";
import * as commands from "../commands";
import * as responses from "../responses";
import * as queries from "../queries";
import http, { createErrorResponse, createResponse } from "./httpBase";

export async function groupDetails(groupId: string): Promise<responses.GroupDetailsResponse> {
  const response = await http.get<responses.GroupDetailsResponse>(`/groups/details/${groupId}`);
  return response.data;
}

export async function groups(userId: string) {
  const response = await http.get<responses.GroupsSummaryResponse>(`/groups/${userId}`);
  return { data: response.data };
}

export async function addGroup(request: commands.AddGroupRequest) {
  const response = await http.post<ApiResponse<string>>("groups/add", request);
  return { data: response.data };
}

export async function updateGroup(request: commands.UpdateGroupRequest) {
  const response = await http.put<ApiResponse<any>>("groups/update", request);
  return { data: response.data };
}

export async function connectGroups(request: commands.ConnectGroupsRequest) {
  const response = await http.put<ApiResponse<any>>("groups/merge", request);
  return { data: response.data };
}

export async function searchGroups(request: queries.SearchGroupsQuery): Promise<GroupSummary[]> {
  try {
    const response = await http.put<GroupSummary[]>("/groups/search", request);
    return response.data;
  } catch (e: any) {
    return e.response.data as any;
  }
}

export async function searchGroupCount(request: queries.SearchGroupsQuery): Promise<number | any> {
  try {
    const response = await http.put<number>(`/groups/search/count`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function saveGroup(request: commands.SaveGroupRequest): Promise<number | any> {
  try {
    const response = await http.post<number>(`/groups/append`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function getGroups(
  request: queries.GetGroupsToLessonQuery
): Promise<ApiResponse<responses.GetGroupToLessonResponse>> {
  try {
    const response = await http.get<responses.GetGroupToLessonResponse>(
      `/groups/lesson/${request.ownerId}`
    );
    return createResponse(response.data);
  } catch (e: any) {
    return createErrorResponse("");
  }
}
