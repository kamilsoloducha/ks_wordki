import "./CardItem.scss";
import { ReactElement } from "react";
import { CardSummary, SideSummary } from "pages/cards/models";

export function CardItem({ card, onClick }: RowModel): ReactElement {
  return (
    <div className="row-container" onClick={() => onClick && onClick(card)}>
      <div className="row-sides">
        <Side side={card.front} />
        <Side side={card.back} />
      </div>
    </div>
  );
}

export interface RowModel {
  card: CardSummary;
  onClick?: (card: CardSummary) => void;
}

function Side({ side }: { side: SideSummary }): ReactElement {
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
