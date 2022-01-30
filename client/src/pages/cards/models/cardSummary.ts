import { CardSide } from "./cardSide";

export interface CardSummary {
  id: number;
  front: CardSide;
  back: CardSide;
}
