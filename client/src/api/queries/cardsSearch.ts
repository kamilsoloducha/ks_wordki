export type CardsSearchQuery = {
  searchingTerm: string
  searchingDrawers: number[]
  lessonIncluded: boolean | null
  isTicked: boolean | null

  pageNumber: number
  pageSize: number
}
