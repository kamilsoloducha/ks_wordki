import LoadingSpinner from 'common/components/LoadingSpinner'
import { ReactElement, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { getGroupsSummary } from 'store/groups/reducer'
import { selectIsLoading } from 'store/groups/selectors'
import { useTitle } from 'common/index'
import { getLanguages } from 'store/lesson/reducer'
import { UserGroups } from 'pages/groups/components/UserGroups'
import { GroupTopBar } from 'pages/groups/components/GroupTopBar'

export default function GroupsPage(): ReactElement {
  useTitle('Wordki - Groups')
  const dispatch = useDispatch()
  const isLoading = useSelector(selectIsLoading)

  useEffect(() => {
    dispatch(getGroupsSummary())
    dispatch(getLanguages())
  }, [dispatch])

  if (isLoading) {
    return <LoadingSpinner />
  }

  return (
    <>
      <GroupTopBar />
      <UserGroups />
    </>
  )
}
