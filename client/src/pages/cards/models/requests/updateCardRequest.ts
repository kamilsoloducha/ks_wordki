export interface UpdateCardRequest {
  userId: string;
  groupId: number;
  cardId: number;
  front: { value: string; example: string; isUsed: boolean; isTicked: boolean };
  back: { value: string; example: string; isUsed: boolean; isTicked: boolean };
}
