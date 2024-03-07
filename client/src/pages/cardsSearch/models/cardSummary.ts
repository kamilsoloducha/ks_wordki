import { Side } from './side'

export interface CardSummary {
  id: string
  groupId: string
  groupName: string
  front: Side
  back: Side
}
