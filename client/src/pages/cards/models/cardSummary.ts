import { CardSide } from "./cardSide";

export interface CardSummary {
  id: string;
  front: CardSide;
  back: CardSide;
  comment: string;
  isTicked: boolean;
}
