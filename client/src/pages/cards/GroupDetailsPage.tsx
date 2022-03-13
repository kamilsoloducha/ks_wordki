import "./GroupDetailsPage.scss";
import { ReactElement, useCallback, useEffect, useState } from "react";
import CardsList from "./components/cardsList/CardsList";
import { CardSummary, SideSummary } from "./models/groupDetailsSummary";
import { useDispatch, useSelector } from "react-redux";
import * as selectors from "store/cards/selectors";
import * as actions from "store/cards/actions";
import * as groupActions from "store/groups/actions";
import { useParams } from "react-router";
import InfoCard from "./components/infoCard/InfoCard";
import Expandable from "common/components/expandable/Expandable";
import { PageChangedEvent } from "common/components/pagination/pageChagnedEvent";
import { CardsFilter } from "./models/cardsFilter";
import GroupDetailsComponent from "./components/groupDetails/GroupDetails";
import { Languages } from "common/models/languages";
import AppendToLessonDialog from "./components/appendToLessonDialog/AppendToLessonDialog";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import GroupDetails from "common/components/dialogs/groupDialog/groupDetails";
import CardDialog from "common/components/dialogs/cardDialog/CardDialog";
import GroupDialog from "common/components/dialogs/groupDialog/GroupDialog";
import ActionsDialog from "common/components/dialogs/actionsDialog/ActionsDialog";
import { Pagination } from "common/components/pagination/Pagination";

const pageSize = 30;

export default function GroupDetailsPage(): ReactElement {
  const allCards = useSelector(selectors.selectCards);
  const filteredCardsFromStore = useSelector(selectors.selectFilteredCards);
  const filterState = useSelector(selectors.selectFilterState);
  const [paginatedCards, setPaginatedCards] = useState<CardSummary[]>([]);
  const [formItem, setFormItem] = useState<FormModel | null>(null);
  const [appendDialog, setAppendDialog] = useState(false);
  const [page, setPage] = useState(1);
  const { groupId } = useParams<{ groupId: string }>();
  const dispatch = useDispatch();
  const isLoading = useSelector(selectors.selectIsLoading);
  const groupDetails = useSelector(selectors.selectGroupDetails);
  const [actionsVisible, setActionsVisible] = useState(false);
  const [editedGroup, setEditedGroup] = useState<any>(null);

  useEffect(() => {
    dispatch(actions.getCards(parseInt(groupId)));
  }, [groupId, dispatch]);

  useEffect(() => {
    const first = (page - 1) * pageSize;
    const last = first + pageSize;
    setPaginatedCards(filteredCardsFromStore.slice(first, last));
  }, [page, filteredCardsFromStore]);

  const onItemSelected = (item: CardSummary) => {
    dispatch(actions.selectCard(item));
    setFormItem(getFormModelFromCardSummary(item));
  };

  const onFormSubmit = (item: FormModel): void => {
    const udpdatedCard = {
      id: item.cardId,
      front: {
        value: item.frontValue,
        example: item.frontExample,
        isUsed: item.frontEnabled,
        isTicked: item.isTicked,
      },
      back: {
        value: item.backValue,
        example: item.backExample,
        isUsed: item.backEnabled,
        isTicked: item.isTicked,
      },
    } as CardSummary;
    if (udpdatedCard.id !== 0) {
      dispatch(actions.updateCard(udpdatedCard));
      setFormItem(null);
    } else {
      dispatch(actions.addCard(udpdatedCard));
      onAddCard();
    }
  };

  const onDelete = () => {
    dispatch(actions.deleteCard());
    setFormItem(null);
  };

  const onCancel = () => {
    dispatch(actions.resetSelectedCard());
    setFormItem(null);
  };

  const onClickSetFilter = (filter: CardsFilter) => {
    if (filter === 1) {
      dispatch(actions.resetFilter());
    } else if (filter === 2) {
      dispatch(actions.setFilterLearning(true));
    } else if (filter === 3) {
      dispatch(actions.setFilterLearning(false));
    } else if (filter >= 4 && filter <= 8) {
      dispatch(actions.setFilterDrawer(filter - 3));
    } else if (filter === 9) {
      dispatch(actions.setFilterIsTicked(true));
    }
  };

  const onAddCard = () => {
    const cardTemplate = {
      id: 0,
      front: { value: "", example: "", isUsed: false },
      back: { value: "", example: "", isUsed: false },
    } as CardSummary;
    dispatch(actions.selectCard(cardTemplate));
    setFormItem(getFormModelFromCardSummary(cardTemplate));
  };

  const onPageChagned = (event: PageChangedEvent) => {
    setPage(event.currectPage);
  };

  const onSearchChanged = useCallback(
    (text: string) => {
      dispatch(actions.setFilterText(text));
    },
    [dispatch]
  );

  const onActionsVisible = () => {
    setActionsVisible(false);
  };

  const onEditGroup = () => {
    const group = {
      id: groupDetails.id,
      name: groupDetails.name,
      front: groupDetails.language1,
      back: groupDetails.language2,
    };
    setEditedGroup(group);
  };

  const onHideGroupDialog = () => {
    setEditedGroup(null);
  };

  const onSubmitGroupDialog = (group: GroupDetails) => {
    dispatch(groupActions.updateGroup(group));
    onHideGroupDialog();
  };

  const appendDialogSubmit = (count: number, languages: number) => {
    dispatch(actions.appendCard(groupId, count, languages));
    setAppendDialog(false);
  };

  const appendDialogHide = () => {
    setAppendDialog(false);
  };

  if (isLoading) {
    return <LoadingSpinner />;
  }

  const acts = [
    { label: "Add card", action: onAddCard },
    { label: "Edit group", action: onEditGroup },
  ];

  return (
    <div className="group-detail-main-container">
      <GroupDetailsComponent
        name={groupDetails.name}
        front={groupDetails.language1}
        back={groupDetails.language2}
        onSettingsClick={() => setActionsVisible(true)}
      />
      <div className="group-details-cards-info-container">
        <div className="group-details-info-card">
          <InfoCard
            label="all cards"
            value={allCards.length}
            classNameOverriden="info-card-blue"
            onClick={() => onClickSetFilter(CardsFilter.All)}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="learning"
            value={getLearningCard(allCards)}
            classNameOverriden="info-card-green"
            onClick={() => onClickSetFilter(CardsFilter.Learning)}
          />
        </div>
        {drawers.map((item) => (
          <div className="group-details-info-card" key={item}>
            <InfoCard
              label={"drawer " + item}
              value={getCardsCountFromDrawer(allCards, item)}
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
            value={2 * allCards.length - getLearningCard(allCards)}
            classNameOverriden="info-card-gray"
            onClick={() => onClickSetFilter(CardsFilter.Waiting)}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="ticked"
            value={getTickedCard(allCards)}
            classNameOverriden="info-card-gray"
            onClick={() => onClickSetFilter(CardsFilter.Ticked)}
          />
        </div>
      </Expandable>
      <div className="group-details-paginator">
        <Pagination
          totalCount={filteredCardsFromStore.length}
          onPageChagned={onPageChagned}
          search={filterState.text}
          onSearchChanged={onSearchChanged}
        />
      </div>
      <div className="group-details-card-list-container">
        <CardsList cards={paginatedCards} onItemSelected={onItemSelected} />
      </div>
      <CardDialog
        card={formItem}
        onHide={onCancel}
        onSubmit={onFormSubmit}
        onDelete={onDelete}
        frontLanguage={Languages[groupDetails.language1]}
        backLanguage={Languages[groupDetails.language2]}
      />
      <GroupDialog group={editedGroup} onHide={onHideGroupDialog} onSubmit={onSubmitGroupDialog} />
      <ActionsDialog isVisible={actionsVisible} onHide={onActionsVisible} actions={acts} />
      <AppendToLessonDialog
        isVisible={appendDialog}
        onHide={appendDialogHide}
        onSubmit={appendDialogSubmit}
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
    isTicked: card.front.isTicked,
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

function getTickedCard(cards: CardSummary[]): number {
  let result = 0;
  cards.forEach((item) => {
    if (item.back.isTicked) result++;
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

function isSideFromDrawer(side: SideSummary, drawer: number): boolean {
  return side.drawer === drawer && side.isUsed;
}

const drawers = [1, 2, 3, 4, 5];
