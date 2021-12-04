import "./GroupDetailsPage.scss";
import { ReactElement, useCallback, useEffect, useState } from "react";
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

export default function GroupDetailsPage(): ReactElement {
  const [formItem, setFormItem] = useState<FormModel | null>(null);
  const [filter, setFilter] = useState(CardsFilter.All);
  const { groupId } = useParams<{ groupId: string }>();
  const dispatch = useDispatch();
  const isLoading = useSelector(selectIsLoading);
  const cardsFromStore = useSelector(selectCards);
  const groupDetails = useSelector(selectGroupDetails);
  // const selectedItem = useSelector(selectSelectedCard);
  const cards = filterCards(cardsFromStore, filter);

  useEffect(() => {
    dispatch(getCards(groupId));
  }, [groupId, dispatch]);

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
  }, []);

  const onClickDrawerInfo = useCallback(
    (drawer: number) => {
      setFilter(3 + drawer);
    },
    [setFilter]
  );

  const onClickFilterAll = useCallback(() => {
    setFilter(CardsFilter.All);
  }, [setFilter]);

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

  const onPageChagned = (event: PageChangedEvent) => {};

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="group-detail-main-container">
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
            onClick={onClickFilterAll}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="learning"
            value={getLearningCard(cardsFromStore)}
            classNameOverriden="info-card-green"
          />
        </div>
        {drawers.map((item) => (
          <div className="group-details-info-card">
            <InfoCard
              label={"drawer " + item}
              value={getCardsCountFromDrawer(cardsFromStore, item)}
              classNameOverriden={"info-card-drawer-" + item}
              onClick={() => onClickDrawerInfo(item)}
            />
          </div>
        ))}
      </div>
      <Pagination totalCount={cards.length} onPageChagned={onPageChagned} />
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
  switch (filter) {
    case CardsFilter.All:
      return cards;
    case CardsFilter.Drawer1:
      return cards.filter((item) => isCardFromDrawer(item, 1));
    case CardsFilter.Drawer2:
      return cards.filter((item) => isCardFromDrawer(item, 2));
    case CardsFilter.Drawer3:
      return cards.filter((item) => isCardFromDrawer(item, 3));
    case CardsFilter.Drawer4:
      return cards.filter((item) => isCardFromDrawer(item, 4));
    case CardsFilter.Drawer5:
      return cards.filter((item) => isCardFromDrawer(item, 5));
    default:
      return cards;
  }
}

function isSideFromDrawer(side: SideSummary, drawer: number): boolean {
  return side.drawer === drawer && side.isUsed;
}

function isCardFromDrawer(card: CardSummary, drawer: number): boolean {
  return (
    isSideFromDrawer(card.front, drawer) || isSideFromDrawer(card.back, drawer)
  );
}

const drawers = [1, 2, 3, 4, 5];
