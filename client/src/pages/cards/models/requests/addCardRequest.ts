export interface AddCardRequest {
  userId: string;
  groupId: string;
  front: { value: string; example: string; isUsed: boolean };
  back: { value: string; example: string; isUsed: boolean };
  comment: string;
}
