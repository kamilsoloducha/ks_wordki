import { ApiResponse } from 'common/models/response'
import { GroupSummary } from 'common/models/groupSummary'
import * as commands from '../commands'
import * as responses from '../responses'
import * as queries from '../queries'
import http from './httpBase'
import { Group } from 'pages/lessonSettings/models/group'

export async function groupDetails(
  groupId: string
): Promise<responses.GroupDetailsResponse | boolean> {
  try {
    const response = await http.get<responses.GroupDetailsResponse>(`/groups/summary/${groupId}`)
    return response.data
  } catch (error: any) {
    return false
  }
}

export async function summaries() {
  const response = await http.get<GroupSummary[]>(`/groups/summaries`)
  return { data: response.data }
}

export async function addGroup(request: commands.AddGroupRequest) {
  const response = await http.post<ApiResponse<string>>('groups/add', request)
  return { data: response.data }
}

export async function updateGroup(groupId: string, request: commands.UpdateGroupRequest) {
  const response = await http.put<ApiResponse<any>>(`groups/update/${groupId}`, request)
  return { data: response.data }
}

export async function connectGroups(request: commands.ConnectGroupsRequest) {
  const response = await http.put<ApiResponse<any>>('groups/merge', request)
  return { data: response.data }
}

export async function searchGroups(request: queries.SearchGroupsQuery): Promise<GroupSummary[]> {
  try {
    const response = await http.put<GroupSummary[]>('/groups/search', request)
    return response.data
  } catch (e: any) {
    return e.response.data as any
  }
}

export async function searchGroupCount(request: queries.SearchGroupsQuery): Promise<number | any> {
  try {
    const response = await http.put<number>(`/groups/search/count`, request)
    return response.data
  } catch (error) {
    return { error }
  }
}

export async function saveGroup(request: commands.SaveGroupRequest): Promise<number | any> {
  try {
    const response = await http.post<number>(`/groups/append`, request)
    return response.data
  } catch (error) {
    return { error }
  }
}

export async function getGroups(): Promise<Group[]> {
  const response = await http.get<Group[]>(`/groups/forlesson`)
  return response.data
}
