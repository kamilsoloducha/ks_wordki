export interface RepeatsQuery {
  ownerId: string;
  count: number;
  questionLanguage: number[] | null;
  groupId: string | null;
  lessonIncluded: boolean;
}
