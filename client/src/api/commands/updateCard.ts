export interface UpdateCardRequest {
  front: { value: string; example: string; isUsed: boolean | null; isTicked: boolean };
  back: { value: string; example: string; isUsed: boolean | null; isTicked: boolean };
  comment: string;
}
