import './GroupsSearch.scss'
import { Fragment, ReactElement } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import * as actions from 'store/groupsSearch/reducer'
import * as selectors from 'store/groupsSearch/selectors'
import LoadingSpinner from 'common/components/loadingSpinner/LoadingSpinner'
import GroupRow from 'pages/groups/components/groupRow/GroupRow'
import { Dialog } from 'primereact/dialog'
import { useTitle } from 'common/index'

export default function GroupsSearchPage(): ReactElement {
  useTitle('Wordki - Groups')
  const dispatch = useDispatch()

  const filter = useSelector(selectors.selectFilter)
  const isSearching = useSelector(selectors.selectIsSearching)
  const groups = useSelector(selectors.selectGroups)
  const selectedGroup = useSelector(selectors.selectSelectedGroup)
  const isCardsLoading = useSelector(selectors.selectIsCardsLoading)
  const cards = useSelector(selectors.selectCards)

  const onGroupNameChanged = (event$: any) => {
    const value = event$.target.value
    dispatch(actions.filterSetName({ name: value }))
  }

  const onGroupSelected = (groupId: string) => {
    const selectedGroup = groups.find((x: any) => x.id === groupId)
    if (!selectedGroup) return
    dispatch(actions.setGroup({ group: selectedGroup }))
  }

  const onDialogHide = () => {
    dispatch(actions.resetSelection())
  }

  const onSave = () => {
    dispatch(actions.saveGroup())
  }

  return (
    <>
      {isSearching && <LoadingSpinner />}
      <div className="groups-search-container">
        <div className="groups-search-filter">
          <form id="seachGroupForm" autoComplete="off" onSubmit={(e) => e.preventDefault()}>
            <input
              id="groupName"
              name="groupName"
              type="search"
              onChange={onGroupNameChanged}
              value={filter}
              placeholder="Search..."
            />
            <button onClick={() => dispatch(actions.search())}>Search</button>
          </form>
        </div>
        <div className="groups-search-results">
          {groups.map((x: any) => (
            <Fragment key={x.id}>
              <GroupRow
                groupSummary={{
                  id: x.id,
                  name: x.name,
                  front: x.front,
                  back: x.back,
                  cardsCount: x.cardsCount
                }}
                onClick={onGroupSelected}
              />
            </Fragment>
          ))}
        </div>
        <Dialog
          onHide={onDialogHide}
          visible={selectedGroup !== null}
          draggable={false}
          footer={Footer({ onDialogHide, onSave })}
        >
          {isCardsLoading && <LoadingSpinner />}
          {cards.map((x: any, index: number) => (
            <Fragment key={index}>
              <p>
                {x.front.value} - {x.back.value}
              </p>
            </Fragment>
          ))}
        </Dialog>
      </div>
    </>
  )
}

function Footer({
  onDialogHide,
  onSave
}: {
  onDialogHide: () => void
  onSave: () => void
}): ReactElement {
  return (
    <div className="search-group-dialog-footer">
      <button onClick={onDialogHide}>Cancel</button>
      <button onClick={onSave}>Save</button>
    </div>
  )
}
