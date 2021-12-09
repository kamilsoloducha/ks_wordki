import GroupDetails from "common/components/groupDialog/groupDetails";
import GroupDialog from "common/components/groupDialog/GroupDialog";
import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  addGroup,
  connectGroups,
  getGroupsSummary,
  resetSelectedItem,
  selectionChanged,
  selectItem,
  updateGroup,
} from "store/groups/actions";
import {
  selectGroups,
  selectIsLoading,
  selectSelectedItem,
} from "store/groups/selectors";
import GroupRow from "./components/groupRow/GroupRow";
import { GroupSummary } from "./models/groupSummary";

export default function GroupsPage(): ReactElement {
  const dispatch = useDispatch();
  const isLoading = useSelector(selectIsLoading);
  const groups = useSelector(selectGroups);
  const selectedItem = useSelector(selectSelectedItem);
  const dialogItem = !selectedItem
    ? (null as any)
    : ({ id: selectedItem.id, name: selectedItem.name } as GroupDetails);

  useEffect(() => {
    dispatch(getGroupsSummary());
  }, [dispatch]);

  if (isLoading) {
    return <>Loading...</>;
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

  const onConnectGroups = () => {
    dispatch(connectGroups());
  };

  const selectGroup = (group: GroupSummary) => {
    dispatch(selectItem(group));
  };

  const onGroupSelected = (id: string, isSelected: boolean) => {
    dispatch(selectionChanged(id, isSelected));
  };

  return (
    <>
      Groups
      <button onClick={onaddgroup}>Add Group</button>
      <button onClick={onConnectGroups}>Connect Groups</button>
      {groups.map((x) => (
        <div key={x.id} onClick={() => selectGroup(x)}>
          <GroupRow
            id={x.id}
            name={x.name}
            cardsCount={x.cardsCount}
            cardsEnalbed={x.cardsEnabled}
            onSelectionChanged={onGroupSelected}
          />
        </div>
      ))}
      <GroupDialog group={dialogItem} onHide={onhide} onSubmit={onsubmit} />
    </>
  );
}
