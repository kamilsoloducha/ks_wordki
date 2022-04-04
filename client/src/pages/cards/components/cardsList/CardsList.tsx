import "./CardsList.scss";
import { CardItem } from "../cardItem/CardItem";
import { CardSummary } from "pages/cards/models";

function CardsList({ cards, onItemSelected }: Model) {
  const onClick = (item: CardSummary) => {
    if (onItemSelected) onItemSelected(item);
  };
  return (
    <>
      {cards.map((x) => (
        <div className="card-item" key={x.id} onClick={() => onClick(x)}>
          <CardItem card={x} />
        </div>
      ))}
    </>
  );
}

export default CardsList;

interface Model {
  cards: CardSummary[];
  onItemSelected?: (item: CardSummary) => void;
}
