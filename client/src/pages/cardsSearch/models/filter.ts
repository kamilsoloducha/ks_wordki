export interface Filter {
  searchingTerm: string
  tickedOnly: boolean | null
  lessonIncluded: boolean | null

  pageNumber: number
  pageSize: number
}
