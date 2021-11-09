import "./CardItem.scss";
import { CardSummary } from "pages/cards/models/groupDetailsSummary";

function CardItem({ card }: Model) {
  return (
    <div className="card-item-container">
      <div className="card-item-side">
        <div className="card-item-value">{card.front.value}</div>
        <div className="card-item-example">{card.front.example}</div>
        <div className="card-item-drawer">{card.front.drawer}</div>
      </div>
      <div className="card-item-side">
        <div className="card-item-value">{card.back.value}</div>
        <div className="card-item-example">{card.back.example}</div>
        <div className="card-item-drawer">{card.back.drawer}</div>
      </div>
      <div className="card-item-comment">{card.comment}</div>
    </div>
  );
}

export default CardItem;

export interface Model {
  card: CardSummary;
}
