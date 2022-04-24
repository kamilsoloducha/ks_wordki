import GroupDetails from "common/components/dialogs/groupDialog/groupDetails";
import { GroupSummary } from "pages/groups/models/groupSummary";

export interface GetGroupsSummarySuccess {
  groups: GroupSummary[];
}

export interface SelectById {
  groupId: string;
}

export interface Select {
  group: GroupSummary;
}

export interface AddGroup {
  group: GroupDetails;
}

export interface UpdateGroup {
  group: GroupDetails;
}
