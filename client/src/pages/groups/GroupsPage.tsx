import "./GroupsPage.scss";
import GroupDetails from "common/components/dialogs/groupDialog/groupDetails";
import GroupDialog from "common/components/dialogs/groupDialog/GroupDialog";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import { PageChangedEvent } from "common/components/pagination/pageChagnedEvent";
import { Pagination } from "common/components/pagination/Pagination";
import { Fragment, ReactElement, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import {
  addGroup,
  getGroupsSummary,
  resetSelectedItem,
  selectItem,
  updateGroup,
} from "store/groups/actions";
import { selectGroups, selectIsLoading, selectSelectedItem } from "store/groups/selectors";
import GroupRow from "./components/groupRow/GroupRow";
import { GroupSummary } from "./models/groupSummary";
import { useTitle } from "common";

const pageSize = 30;

export default function GroupsPage(): ReactElement {
  useTitle("Wordki - Groups");
  const dispatch = useDispatch();
  const history = useHistory();
  const [page, setPage] = useState(1);
  const [paginatedItems, setPaginatedItems] = useState<GroupSummary[]>([]);
  const isLoading = useSelector(selectIsLoading);
  const groups = useSelector(selectGroups);
  const selectedItem = useSelector(selectSelectedItem);
  const dialogItem = !selectedItem
    ? (null as any)
    : ({ id: selectedItem.id, name: selectedItem.name } as GroupDetails);

  useEffect(() => {
    dispatch(getGroupsSummary());
  }, [dispatch]);

  useEffect(() => {
    const first = (page - 1) * pageSize;
    const last = first + pageSize;
    setPaginatedItems(groups.slice(first, last));
  }, [page, groups]);

  if (isLoading) {
    return <LoadingSpinner />;
  }

  const onhide = () => {
    dispatch(resetSelectedItem());
  };

  const onsubmit = (group: GroupDetails) => {
    dispatch(group.id ? updateGroup(group) : addGroup(group));
  };

  const onaddgroup = () => {
    dispatch(selectItem({} as GroupSummary));
  };

  const selectGroup = (group: GroupSummary) => {
    history.push("/cards/" + group.id);
  };

  const onPageChagned = (event: PageChangedEvent) => {
    setPage(event.currectPage);
  };

  const onSearchGroup = () => {
    history.push("/groups/search");
  };

  return (
    <>
      <div className="groups-action-container">
        <button data-testid="new-group-button" onClick={onaddgroup}>
          Create new group
        </button>
        <button onClick={onSearchGroup}>Search from existing</button>
      </div>
      {paginatedItems.map((x) => (
        <Fragment key={x.id}>
          <GroupRow
            onClick={() => selectGroup(x)}
            groupSummary={{
              id: x.id,
              name: x.name,
              front: x.front,
              back: x.back,
              cardsCount: x.cardsCount,
              cardsEnabled: x.cardsEnabled ?? 0,
            }}
          />
        </Fragment>
      ))}
      <Pagination totalCount={groups.length} onPageChagned={onPageChagned} />
      <GroupDialog group={dialogItem} onHide={onhide} onSubmit={onsubmit} />
    </>
  );
}
