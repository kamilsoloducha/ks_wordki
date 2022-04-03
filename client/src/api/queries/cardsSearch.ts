export interface CardsSearchQuery {
  ownerId: string;
  searchingTerm: string;
  searchingDrawers: number[];
  lessonIncluded: boolean | null;
  onlyTicked: boolean;

  pageNumber: number;
  pageSize: number;
}
