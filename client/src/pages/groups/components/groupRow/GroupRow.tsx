import { getLanguageByType } from "common/models/languages";
import "./GroupRow.scss";

export default function GroupRow({ groupSummary, onClick }: Model) {
  const front = getLanguageByType(groupSummary.front);
  const back = getLanguageByType(groupSummary.back);

  const onClickHandle = () => {
    if (onClick) {
      onClick(groupSummary.id);
    }
  };

  return (
    <div className="group-row-container" onClick={onClickHandle}>
      <div className="group-row-name">
        <b>{groupSummary.name}</b>
      </div>
      <div className="group-row-cards">{groupSummary.cardsCount}</div>
    </div>
  );
}

interface Model {
  groupSummary: GroupSummary;

  onClick?: (id: string) => void;
}

interface GroupSummary {
  id: string;
  name: string;
  front: number;
  back: number;
  cardsCount: number;
  cardsEnabled?: number;
}
