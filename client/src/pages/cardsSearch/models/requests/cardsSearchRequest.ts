export interface CardsSearchRequest {
  ownerId: string;
  searchingTerm: string;
  searchingDrawers: number[];
  lessonIncluded: boolean | null;
  onlyTicked: boolean;

  pageNumber: number;
  pageSize: number;
}
