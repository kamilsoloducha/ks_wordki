import { CardSide } from "./cardSide";

export interface CardSummary {
  id: number;
  groupId: number;
  front: CardSide;
  back: CardSide;
}
