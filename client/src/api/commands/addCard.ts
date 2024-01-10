export type AddCardRequest = {
  front: { value: string; example: string; isUsed: boolean };
  back: { value: string; example: string; isUsed: boolean };
  comment: string;
};
