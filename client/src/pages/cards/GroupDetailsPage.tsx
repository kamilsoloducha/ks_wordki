import "./GroupDetailsPage.scss";
import { ReactElement, useEffect } from "react";
import CardsList from "./components/cardsList/CardsList";
import GroupDetails from "./components/groupDetails/GroupDetails";
import { CardSummary } from "./models/groupDetailsSummary";
import { useDispatch, useSelector } from "react-redux";
import {
  selectCards,
  selectGroupDetails,
  selectIsLoading,
  selectSelectedCard,
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

function GroupDetailsPage(): ReactElement {
  const { groupId } = useParams<{ groupId: string }>();
  const dispatch = useDispatch();
  const isLoading = useSelector(selectIsLoading);
  const cards = useSelector(selectCards);
  const groupDetails = useSelector(selectGroupDetails);
  const selectedItem = useSelector(selectSelectedCard);

  useEffect(() => {
    dispatch(getCards(groupId));
  }, [groupId, dispatch]);

  const onItemSelected = (item: CardSummary) => {
    dispatch(selectCard(item));
  };

  const onFormSubmit = (item: CardSummary): void => {
    if (item.id) {
      dispatch(updateCard(item));
    } else {
      dispatch(addCard(item));
    }
  };

  const onDelete = () => {
    dispatch(deleteCard());
  };

  const onCancel = () => {
    dispatch(resetSelectedCard());
  };

  const onAddCard = () => {
    const cardTemplate = {
      front: { value: "", example: "", isUsed: false },
      back: { value: "", example: "", isUsed: false },
      comment: "",
    } as CardSummary;
    dispatch(selectCard(cardTemplate));
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
        card={selectedItem}
        onHide={onCancel}
        onSubmit={onFormSubmit}
        onDelete={onDelete}
      />
    </div>
  );
}

export default GroupDetailsPage;
