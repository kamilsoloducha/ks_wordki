export interface UpdateCardRequest {
  userId: string;
  groupId: string;
  cardId: string;
  front: { value: string; example: string; isUsed: boolean | null; isTicked: boolean };
  back: { value: string; example: string; isUsed: boolean | null; isTicked: boolean };
}
