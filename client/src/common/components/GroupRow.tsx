import { GroupSummary } from 'common/models/groupSummary'

type GroupRowProps = {
  groupSummary: GroupSummary
  onClick?: (groupSummary: GroupSummary) => void
}

export function GroupRow({ groupSummary, onClick }: GroupRowProps) {
  const onClickHandle = () => {
    if (onClick) {
      onClick(groupSummary)
    }
  }

  return (
    <div
      className="flex w-full cursor-pointer items-center border-b border-b-lighter-a-bit px-1 py-3 hover:bg-accent-dark"
      onClick={onClickHandle}
    >
      <div className="ms-3 text-center text-3xl">
        <b>{groupSummary.name}</b>
      </div>
      <div className="me-5 ms-auto text-sm">{groupSummary.cardsCount}</div>
    </div>
  )
}
