import { SideSummary } from "./sideSummary";

export interface CardSummary {
  id: string;
  front: SideSummary;
  back: SideSummary;
}
