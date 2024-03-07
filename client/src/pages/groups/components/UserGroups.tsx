import { selectGroups } from 'store/groups/selectors'
import { ReactNode } from 'react'
import { GroupsList } from 'common/components/GroupList'
import { useNavigate } from 'react-router-dom'
import { useAppSelector } from 'store/store'
import { GroupSummary } from 'common/models/groupSummary'

export function UserGroups(): ReactNode {
  const groups = useAppSelector(selectGroups)
  const navigate = useNavigate()

  const onGroupClick = (group: GroupSummary) => {
    navigate(`/cards/${group.id}`)
  }

  function uAppseNavigate() {
    throw new Error('Function not implemented.')
  }
  return <GroupsList items={groups} onItemClick={onGroupClick} />
}
