export interface UpdateCardRequest {
  userId: string;
  groupId: string;
  cardId: string;
  front: { value: string; example: string; isUsed: boolean };
  back: { value: string; example: string; isUsed: boolean };
  comment: string;
  isTicked: boolean;
}
