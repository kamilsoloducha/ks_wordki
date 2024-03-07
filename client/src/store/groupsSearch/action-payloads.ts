import { GroupSummary } from 'common/models/groupSummary'
import { CardSummary } from 'pages/groupsSearch/models/cardSummary'

export interface SearchSuccess {
  groups: GroupSummary[]
  groupsCount: number
}

export interface FilterSetName {
  name: string
}

export interface SetGroup {
  group: GroupSummary
}

export interface GetCardsSuccess {
  cards: CardSummary[]
}
