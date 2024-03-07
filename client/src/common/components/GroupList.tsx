import { GroupSummary } from 'common/models/groupSummary'
import { GroupRow } from 'common/components/GroupRow'
import { Pagination } from 'common/components/Pagination'
import { useLocalSettingsStorage } from 'common/hooks/useSettingsStorage'
import { getPage } from 'common/services/pagination'
import { Fragment, ReactNode, useState } from 'react'

type GroupsListProps = {
  items: GroupSummary[]
  onItemClick: (item: GroupSummary) => void
}

export function GroupsList({ items, onItemClick }: GroupsListProps): ReactNode {
  const [pageNumber, setPageNumber] = useState(1)
  const [pageSize, setPageSize] = useState(
    useLocalSettingsStorage().get()?.paginationPageSize ?? 10
  )

  const paginatedItems = getPage(items, pageSize, pageNumber)

  return (
    <>
      <div className="pb-1">
        <Pagination
          totalCount={items.length}
          onPageChagned={(e) => setPageNumber(e.currectPage)}
          onPageSizeChanged={(e) => setPageSize(e)}
        />
      </div>
      {paginatedItems.map((x) => (
        <Fragment key={x.id}>
          <GroupRow
            onClick={onItemClick}
            groupSummary={{
              id: x.id,
              name: x.name,
              front: x.front,
              back: x.back,
              cardsCount: x.cardsCount,
              cardsEnabled: x.cardsEnabled ?? 0
            }}
          />
        </Fragment>
      ))}
    </>
  )
}
