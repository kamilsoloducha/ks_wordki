import "./CardsList.scss";
import { CardSummary } from "pages/cards/models/groupDetailsSummary";
import CardItem from "../cardItem/CardItem";

function CardsList({ cards, onItemSelected, onChangeUsage }: Model) {
  const onClick = (item: CardSummary) => {
    if (onItemSelected) onItemSelected(item);
  };
  return (
    <>
      {cards.map((x) => (
        <div className="card-item" key={x.id} onClick={() => onClick(x)}>
          <CardItem card={x} direction={1} onChangeUsage={onChangeUsage} />
        </div>
      ))}
    </>
  );
}

export default CardsList;

interface Model {
  cards: CardSummary[];
  onItemSelected?: (item: CardSummary) => void;
  onChangeUsage?: (cardId: string, side: number) => void;
}
