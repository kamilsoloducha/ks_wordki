export function getPage<TType>(items: TType[], pageSize: number, pageNumber: number): TType[] {
  const first = (pageNumber - 1) * pageSize
  const last = first + pageSize
  return items.slice(first, last)
}
