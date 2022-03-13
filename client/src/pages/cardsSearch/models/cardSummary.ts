import { Side } from "./side";

export interface CardSummary {
  id: number;
  groupId: number;
  groupName: string;
  front: Side;
  back: Side;
}
