import "./GroupDetailsPage.scss";
import { ReactElement, useCallback, useEffect, useRef, useState } from "react";
import CardsList from "./components/cardsList/CardsList";
import GroupDetails from "./components/groupDetails/GroupDetails";
import { CardSummary, SideSummary } from "./models/groupDetailsSummary";
import { useDispatch, useSelector } from "react-redux";
import {
  selectCards,
  selectGroupDetails,
  selectIsLoading,
} from "store/cards/selectors";
import { useParams } from "react-router";
import {
  addCard,
  deleteCard,
  getCards,
  resetSelectedCard,
  selectCard,
  updateCard,
} from "store/cards/actions";
import CardDialog from "common/components/cardDialog/CardDialog";
import Pagination from "common/components/pagination/Pagination";
import { PageChangedEvent } from "common/components/pagination/pageChagnedEvent";
import { FormModel } from "common/components/cardDialog/CardForm";
import InfoCard from "./components/infoCard/InfoCard";
import { CardsFilter } from "./models/cardsFilter";
import Expandable from "common/components/expandable/Expandable";

export default function GroupDetailsPage(): ReactElement {
  const containerRef = useRef<any>(null);
  const [formItem, setFormItem] = useState<FormModel | null>(null);
  const [filter, setFilter] = useState(CardsFilter.All);
  const [page, setPage] = useState(1);
  const [filteredCardsCount, setFilteredCardsCount] = useState(0);
  const [cards, setCards] = useState<CardSummary[]>([]);
  const { groupId } = useParams<{ groupId: string }>();
  const dispatch = useDispatch();
  const isLoading = useSelector(selectIsLoading);
  const cardsFromStore = useSelector(selectCards);
  const groupDetails = useSelector(selectGroupDetails);
  console.log();

  useEffect(() => {
    dispatch(getCards(groupId));
  }, [groupId, dispatch]);

  useEffect(() => {
    const filteredCards = filterCards(cardsFromStore, filter);
    setFilteredCardsCount(filteredCards.length);
    const first = (page - 1) * 10;
    const last = first + 10;
    setCards(filteredCards.slice(first, last));
  }, [filter, cardsFromStore, page]);

  const onItemSelected = (item: CardSummary) => {
    dispatch(selectCard(item));
    setFormItem(getFormModelFromCardSummary(item));
  };

  const onFormSubmit = (item: FormModel): void => {
    const udpdatedCard = {
      id: item.cardId,
      comment: item.comment,
      front: {
        value: item.frontValue,
        example: item.frontExample,
        isUsed: item.frontEnabled,
      },
      back: {
        value: item.backValue,
        example: item.backExample,
        isUsed: item.backEnabled,
      },
    } as CardSummary;
    if (udpdatedCard.id) {
      dispatch(updateCard(udpdatedCard));
      setFormItem(null);
    } else {
      dispatch(addCard(udpdatedCard));
      onAddCard();
    }
  };

  const onDelete = () => {
    dispatch(deleteCard());
    setFormItem(null);
  };

  const onCancel = () => {
    dispatch(resetSelectedCard());
    setFormItem(null);
  };

  const onSettingsClick = useCallback(() => {
    console.log("openMore");
    onAddCard();
  }, []);

  const onClickSetFilter = (filter: CardsFilter) => {
    setFilter(filter);
  };

  const onAddCard = () => {
    const cardTemplate = {
      id: "",
      front: { value: "", example: "", isUsed: false },
      back: { value: "", example: "", isUsed: false },
      comment: "",
    } as CardSummary;
    dispatch(selectCard(cardTemplate));
    setFormItem(getFormModelFromCardSummary(cardTemplate));
  };

  const onPageChagned = (event: PageChangedEvent) => {
    setPage(event.currectPage);
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="group-detail-main-container" ref={containerRef}>
      <GroupDetails
        name={groupDetails.name}
        front={groupDetails.language1}
        back={groupDetails.language2}
        onSettingsClick={onSettingsClick}
      />
      <div className="group-details-cards-info-container">
        <div className="group-details-info-card">
          <InfoCard
            label="all cards"
            value={cardsFromStore.length}
            classNameOverriden="info-card-blue"
            onClick={() => onClickSetFilter(CardsFilter.All)}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="learning"
            value={getLearningCard(cardsFromStore)}
            classNameOverriden="info-card-green"
            onClick={() => onClickSetFilter(CardsFilter.Learning)}
          />
        </div>
        {drawers.map((item) => (
          <div className="group-details-info-card" key={item}>
            <InfoCard
              label={"drawer " + item}
              value={getCardsCountFromDrawer(cardsFromStore, item)}
              classNameOverriden={"info-card-drawer-" + item}
              onClick={() => onClickSetFilter(3 + item)}
            />
          </div>
        ))}
      </div>
      <Expandable>
        <div className="group-details-info-card">
          <InfoCard
            label="waiting"
            value={2 * cardsFromStore.length - getLearningCard(cardsFromStore)}
            classNameOverriden="info-card-gray"
            onClick={() => onClickSetFilter(CardsFilter.Waiting)}
          />
        </div>
      </Expandable>
      <div className="group-details-paginator">
        <Pagination
          totalCount={filteredCardsCount}
          onPageChagned={onPageChagned}
        />
      </div>

      <div className="group-details-card-list-container">
        <CardsList cards={cards} onItemSelected={onItemSelected} />
      </div>

      <CardDialog
        card={formItem}
        onHide={onCancel}
        onSubmit={onFormSubmit}
        onDelete={onDelete}
      />
    </div>
  );
}

function getFormModelFromCardSummary(card: CardSummary): FormModel {
  return {
    cardId: card.id,
    frontValue: card.front.value,
    frontExample: card.front.example,
    frontEnabled: card.front.isUsed,
    backValue: card.back.value,
    backExample: card.back.example,
    backEnabled: card.back.isUsed,
    comment: card.comment,
  } as FormModel;
}

function getLearningCard(cards: CardSummary[]): number {
  let result = 0;
  cards.forEach((item) => {
    if (item.front.isUsed) result++;
    if (item.back.isUsed) result++;
  });
  return result;
}

function getCardsCountFromDrawer(cards: CardSummary[], drawer: number): number {
  let result = 0;
  cards.forEach((item) => {
    if (isSideFromDrawer(item.front, drawer)) result++;
    if (isSideFromDrawer(item.back, drawer)) result++;
  });
  return result;
}

function filterCards(cards: CardSummary[], filter: CardsFilter): CardSummary[] {
  let result = [];
  switch (filter) {
    case CardsFilter.All:
      result = cards;
      break;
    case CardsFilter.Learning:
      result = cards.filter((item) => isCardInUsed(item));
      break;
    case CardsFilter.Waiting:
      result = cards.filter((item) => isCardNotInUsed(item));
      break;
    case CardsFilter.Drawer1:
      result = cards.filter((item) => isCardFromDrawer(item, 1));
      break;
    case CardsFilter.Drawer2:
      result = cards.filter((item) => isCardFromDrawer(item, 2));
      break;
    case CardsFilter.Drawer3:
      result = cards.filter((item) => isCardFromDrawer(item, 3));
      break;
    case CardsFilter.Drawer4:
      result = cards.filter((item) => isCardFromDrawer(item, 4));
      break;
    case CardsFilter.Drawer5:
      result = cards.filter((item) => isCardFromDrawer(item, 5));
      break;
    default:
      result = cards;
      break;
  }
  return result;
}

function isSideFromDrawer(side: SideSummary, drawer: number): boolean {
  return side.drawer === drawer && side.isUsed;
}

function isCardFromDrawer(card: CardSummary, drawer: number): boolean {
  return (
    isSideFromDrawer(card.front, drawer) || isSideFromDrawer(card.back, drawer)
  );
}

function isCardInUsed(card: CardSummary): boolean {
  return card.front.isUsed || card.back.isUsed;
}

function isCardNotInUsed(card: CardSummary): boolean {
  return !card.front.isUsed || !card.back.isUsed;
}

const drawers = [1, 2, 3, 4, 5];
