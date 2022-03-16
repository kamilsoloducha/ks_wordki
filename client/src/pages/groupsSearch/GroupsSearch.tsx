import "./GroupsSearch.scss";
import { Fragment, ReactElement } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as actions from "store/groupsSearch/actions";
import * as selectors from "store/groupsSearch/selectors";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import GroupRow from "pages/groups/components/groupRow/GroupRow";

export default function GroupsSearch(): ReactElement {
  const dispatch = useDispatch();
  const filter = useSelector(selectors.selectFilter);
  const isSearching = useSelector(selectors.selectIsSearching);
  const groups = useSelector(selectors.selectGroups);

  const onGroupNameChanged = (event$: any) => {
    const value = event$.target.value;
    dispatch(actions.filterSetName(value));
  };

  if (isSearching) {
    return <LoadingSpinner />;
  }

  return (
    <div className="groups-search-container">
      <div className="groups-search-filter">
        <form id="seachGroupForm" autoComplete="off" onSubmit={(e) => e.preventDefault()}>
          <div>
            <label htmlFor="groupName">Name</label>
            <input
              id="groupName"
              name="groupName"
              type="search"
              onChange={onGroupNameChanged}
              value={filter}
            />
          </div>
          <button onClick={() => dispatch(actions.search())}>Search</button>
        </form>
      </div>
      <div className="groups-search-results">
        {groups.map((x) => (
          <Fragment key={x.id}>
            <GroupRow
              groupSummary={{
                id: x.id,
                name: x.name,
                front: x.front,
                back: x.back,
                cardsCount: x.cardsCount,
              }}
            />
          </Fragment>
        ))}
      </div>
    </div>
  );
}