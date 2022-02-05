export interface GetRepeatsRequest {
  ownerId: string;
  count: number;
  questionLanguage: number | null;
  groupId: number | null;
  lessonIncluded: boolean;
}
