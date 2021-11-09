import GroupDetails from "common/components/groupDialog/groupDetails";
import GroupDialog from "common/components/groupDialog/GroupDialog";
import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getGroupsSummary, selectItem } from "store/groups/actions";
import {
  selectGroups,
  selectIsLoading,
  selectSelectedItem,
} from "store/groups/selectors";
import GroupRow from "./components/groupRow/GroupRow";
import { GroupSummary } from "./models/groupSummary";

function GroupsPage(): ReactElement {
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

  const onhide = () => {};
  const onsubmit = (group: GroupDetails) => {
    console.log(group);
  };

  const onaddgroup = () => {
    dispatch(selectItem({} as GroupSummary));
  };

  const selectGroup = (group: GroupSummary) => {
    dispatch(selectItem(group));
  };

  return (
    <>
      Groups
      <button onClick={onaddgroup}>Add Group</button>
      {groups.map((x) => (
        <div key={x.id} onClick={() => selectGroup(x)}>
          <GroupRow
            id={x.id}
            name={x.name}
            cardsCount={x.cardsCount}
            cardsEnalbed={x.cardsEnabled}
          />
        </div>
      ))}
      <GroupDialog group={dialogItem} onHide={onhide} onSubmit={onsubmit} />
    </>
  );
}

export default GroupsPage;
