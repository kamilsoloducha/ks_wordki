import "./CardItem.scss";
import { CardSummary } from "pages/cards/models/groupDetailsSummary";

function CardItem({ card, direction }: Model) {
  const [first, second] =
    direction == 1 ? [card.front, card.back] : [card.back, card.front];
  return (
    <div className="card-item-container">
      <div className="card-item-value">
        <b>{first.value}</b>
        <b>{second.value}</b>
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
}
