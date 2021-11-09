export default interface RegisterAnswerRequest {
  userId: string;
  groupId: string;
  cardId: string;
  side: number;
  result: number;
}
