export interface GetRepeatsRequest {
  ownerId: string;
  count: number;
  questionLanguage: number[] | null;
  groupId: string | null;
  lessonIncluded: boolean;
}
