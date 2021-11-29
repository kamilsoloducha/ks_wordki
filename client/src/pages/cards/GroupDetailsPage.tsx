import "./GroupDetailsPage.scss";
import { ReactElement, useEffect, useState } from "react";
import CardsList from "./components/cardsList/CardsList";
import GroupDetails from "./components/groupDetails/GroupDetails";
import { CardSummary } from "./models/groupDetailsSummary";
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

function GroupDetailsPage(): ReactElement {
  const [formItem, setFormItem] = useState<FormModel | null>(null);
  const { groupId } = useParams<{ groupId: string }>();
  const dispatch = useDispatch();
  const isLoading = useSelector(selectIsLoading);
  const cards = useSelector(selectCards);
  const groupDetails = useSelector(selectGroupDetails);
  // const selectedItem = useSelector(selectSelectedCard);

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
    console.log("Page changed", event);
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="group-detail-main-container">
      <div id="group-details">
        <GroupDetails
          name={groupDetails.name}
          front={groupDetails.language1}
          back={groupDetails.language2}
        />
        <button onClick={onAddCard}>Add Card</button>
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

export default GroupDetailsPage;

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
