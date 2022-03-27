import { CardSide } from "./cardSide";

export interface CardSummary {
  id: string;
  groupId: string;
  front: CardSide;
  back: CardSide;
}
