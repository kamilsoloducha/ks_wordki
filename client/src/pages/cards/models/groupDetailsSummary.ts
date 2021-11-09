export default interface GroupDetailsSummary {
  id: string;
  name: string;
  front: number;
  back: number;
  cards: CardSummary[];
}

export interface CardSummary {
  id: string;
  front: SideSummary;
  back: SideSummary;
  comment: string;
}

export interface SideSummary {
  value: string;
  example: string;
  drawer: number;
  isUsed: boolean;
}
