export interface ApiResponse<TBody> {
  error: string
  isCorrect: boolean
  response: TBody
}
