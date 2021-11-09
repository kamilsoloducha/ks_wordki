import { CardSummary } from "./cardSummary";

export interface GroupDetailsResponse {
  id: string;
  name: string;
  front: number;
  back: number;
  cards: CardSummary[];
}
