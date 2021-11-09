import { useHistory } from "react-router";
import "./GroupRow.scss";
import Model from "./model";

function GroupRow({ id, name, cardsCount, cardsEnalbed }: Model) {
  const history = useHistory();

  const navigateToCards = () => {
    history.push("/cards/" + id);
  };

  return (
    <div className="group-row-container" onClick={navigateToCards}>
      <div className="group-row-name">{name}</div>
      <div className="group-row-cards">
        {cardsCount}/{cardsEnalbed}
      </div>
    </div>
  );
}

export default GroupRow;
