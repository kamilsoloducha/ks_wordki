import { SyntheticEvent } from "react";
import { useHistory } from "react-router";
import "./GroupRow.scss";

export default function GroupRow({
  id,
  name,
  cardsCount,
  cardsEnalbed,
  onSelectionChanged,
}: Model) {
  const history = useHistory();

  const navigateToCards = (e: SyntheticEvent) => {
    history.push("/cards/" + id);
  };

  const onCheckboxClick = (e: any) => {
    if (onSelectionChanged) onSelectionChanged(id, e.target.checked);
  };

  return (
    <div className="group-row-container" onClick={navigateToCards}>
      <input
        type="checkbox"
        onChange={onCheckboxClick}
        onClick={(e) => {
          e.stopPropagation();
        }}
      />
      <div className="group-row-name">{name}</div>
      <div className="group-row-cards">
        {cardsCount}/{cardsEnalbed}
      </div>
    </div>
  );
}

interface Model {
  id: number;
  name: string;
  cardsCount: number;
  cardsEnalbed: number;
  onSelectionChanged?: (id: number, isSelected: boolean) => void;
}
