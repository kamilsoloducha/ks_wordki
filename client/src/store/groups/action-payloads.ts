import { GroupFormModel } from 'common/components/GroupForm'
import { GroupSummary } from 'pages/groups/models/groupSummary'

export interface GetGroupsSummarySuccess {
  groups: GroupSummary[]
}

export interface SelectById {
  groupId: string
}

export interface Select {
  group: GroupSummary
}

export interface AddGroup {
  group: GroupFormModel
}

export interface UpdateGroup {
  group: GroupFormModel
}
