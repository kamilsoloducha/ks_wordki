import { GroupFormModel } from 'common/components/GroupForm'
import { addGroup } from 'store/groups/reducer'
import { selectLanguages } from 'store/lesson/selectors'
import { useAppDispatch, useAppSelector } from 'store/store'
import { ReactNode, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import GroupDialog from 'common/components/GroupDialog'

export function GroupTopBar(): ReactNode {
  const history = useNavigate()
  const [dialogGroup, setDialogGroup] = useState<GroupFormModel | undefined>(undefined)
  const dispatch = useAppDispatch()
  const languages = useAppSelector(selectLanguages)

  const onhide = () => {
    setDialogGroup(undefined)
  }

  const onsubmit = (group: GroupFormModel) => {
    dispatch(addGroup({ group }))
    setDialogGroup(undefined)
  }

  const onaddgroup = () => {
    setDialogGroup({} as GroupFormModel)
  }

  const onSearchGroup = () => {
    history('/groups/search')
  }

  return (
    <>
      <div className="g-2 relative mb-2 flex w-full">
        <button
          data-testid="new-group-button"
          className="grow cursor-pointer rounded-md border-none p-4 text-accent-super-light outline-none hover:bg-accent-light"
          onClick={onaddgroup}
        >
          Create new group
        </button>
        <button
          className="grow cursor-pointer rounded-md border-none p-4 text-accent-super-light outline-none hover:bg-accent-light"
          onClick={onSearchGroup}
        >
          Search from existing
        </button>
      </div>
      <GroupDialog cardSides={languages} group={dialogGroup} onHide={onhide} onSubmit={onsubmit} />
    </>
  )
}
