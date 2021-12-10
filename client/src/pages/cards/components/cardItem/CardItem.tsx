import "./CardItem.scss";
import {
  CardSummary,
  SideSummary,
} from "pages/cards/models/groupDetailsSummary";
import Drawer from "../drawer/Drawer";

function CardItem({ card, direction, onChangeUsage }: Model) {
  const [first, second] =
    direction === 1 ? [card.front, card.back] : [card.back, card.front];

  const onDrawerClick = (side: number) => {
    if (!onChangeUsage) return;
    onChangeUsage(card.id, side);
  };

  return (
    <div className="card-item-container">
      <div className="card-item-value">
        <Drawer
          drawer={first.drawer}
          isUsed={first.isUsed}
          onClick={() => {
            onDrawerClick(1);
          }}
        />
        <b>{first.value} </b>
        <b>{second.value}</b>
        <Drawer
          drawer={second.drawer}
          isUsed={second.isUsed}
          onClick={() => {
            onDrawerClick(2);
          }}
        />
      </div>
      {first.example && second.example && (
        <>
          <hr />
          <div className="card-item-example">
            &bull;
            <div className="card-item-example-container">
              <div className="card-item-example-first">{first.example}</div>
              <div className="card-item-example-second">{second.example}</div>
            </div>
          </div>
        </>
      )}
    </div>
  );
}

export default CardItem;

export interface Model {
  card: CardSummary;
  direction: number;
  onChangeUsage?: (cardId: string, side: number) => void;
}
