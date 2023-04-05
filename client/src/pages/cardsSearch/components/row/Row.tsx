import "./Row.scss";
import { ReactElement } from "react";
import { CardSummary, Side as SideModel } from "pages/cardsSearch/models";
import { getLanguageByType } from "common/models/languages";

export function Row({ card, onClick }: RowModel): ReactElement {
  const frontLanguage = getLanguageByType(card.front.lang);
  const backLanguage = getLanguageByType(card.back.lang);

  return (
    <div className="row-container" onClick={() => onClick && onClick(card)}>
      <div className="row-sides">
        <Side side={card.front} />
        <Side side={card.back} />
      </div>
      <div className="row-group-details">
        <p>{card.groupName}</p>
      </div>
    </div>
  );
}

export interface RowModel {
  card: CardSummary;
  onClick?: (card: CardSummary) => void;
}

function Side({ side }: { side: SideModel }): ReactElement {
  return (
    <div className={`row-side-container ${drawerClassName(side.drawer)}`}>
      <div className="row-side-value">
        <strong>{side.value}</strong>
      </div>
      <div className="row-side-example">{side.example}</div>
    </div>
  );
}

function drawerClassName(drawer: number): string {
  return "drawer" + drawer;
}
