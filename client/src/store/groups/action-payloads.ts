import { GroupSummary } from '@/src/common/models/groupSummary'
import { GroupFormModel } from 'common/components/GroupForm'

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
