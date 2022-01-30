export interface GroupDetailsResponse {
  id: number;
  name: string;
  front: number;
  back: number;
}

export default interface GroupDetailsSummary {
  id: string;
  name: string;
  front: number;
  back: number;
  cards: CardSummary[];
}

export interface CardSummary {
  id: number;
  front: SideSummary;
  back: SideSummary;
}

export interface SideSummary {
  type: number;
  value: string;
  example: string;
  comment: string;
  drawer: number;
  isUsed: boolean;
  isTicked: boolean;
}
