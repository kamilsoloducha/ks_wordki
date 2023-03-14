export interface RepeatsQuery {
  ownerId: string;
  count: number;
  questionLanguage: string[] | null;
  groupId: string | null;
  lessonIncluded: boolean;
}
