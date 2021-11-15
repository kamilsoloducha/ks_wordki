import { SyntheticEvent } from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router";
import { selectItemById } from "store/groups/actions";
import "./GroupRow.scss";
import Model from "./model";

function GroupRow({ id, name, cardsCount, cardsEnalbed }: Model) {
  const history = useHistory();
  const dispatch = useDispatch();

  const navigateToCards = (e: SyntheticEvent) => {
    history.push("/cards/" + id);
    e.stopPropagation();
  };

  const editGroup = () => {
    dispatch(selectItemById(id));
  };

  return (
    <div className="group-row-container" onClick={editGroup}>
      <div className="group-row-name">{name}</div>
      <div className="group-row-cards">
        {cardsCount}/{cardsEnalbed}
      </div>
      <button onClick={navigateToCards}>Karty</button>
    </div>
  );
}

export default GroupRow;
