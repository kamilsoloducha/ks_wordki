import * as selectors from "store/cardsSearch/selectors";
import * as actions from "store/cardsSearch/actions";
import { Fragment, ReactElement, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Pagination } from "common/components/pagination/Pagination";
import { PageChangedEvent } from "common/components/pagination/pageChagnedEvent";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import CardDialog from "common/components/dialogs/cardDialog/CardDialog";
import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { CardsOverview, CardSummary } from "./models";
import { Row } from "./components/row/Row";

export default function CardsPage(): ReactElement {
  const dispatch = useDispatch();
  const cards = useSelector(selectors.selectCards);
  const cardsCount = useSelector(selectors.selectCardsCount);
  const filter = useSelector(selectors.selectFilter);
  const isSearching = useSelector(selectors.selectIsSearching);
  const overview = useSelector(selectors.selectOverview);

  const [selectedItem, setSelectedItem] = useState<CardSummary | null>(null);

  useEffect(() => {
    dispatch(actions.getOverview());
    dispatch(actions.search());
  }, [dispatch]);

  const onPageChagned = (event: PageChangedEvent) => {
    dispatch(actions.filterSetPagination(event.currectPage, event.count));
  };

  const onSearchChanged = (searchingTerm: string) => {
    dispatch(actions.filterSetTerm(searchingTerm));
  };

  const setPageSize = (size: number) => {
    dispatch(actions.filterSetPagination(filter.pageNumber, size));
  };

  const clearFilters = () => {
    dispatch(actions.filterReset());
  };

  const tickedOnly = () => {
    dispatch(actions.filterSetTickedOnly(true));
  };

  const lessonIncludedOnly = () => {
    dispatch(actions.filterSetLessonIncluded(true));
  };

  const waitingOnly = () => {
    dispatch(actions.filterSetLessonIncluded(false));
  };

  const onItemSelected = (card: CardSummary) => {
    setSelectedItem(card);
  };

  const onDelete = (model: FormModel | null) => {
    if (!selectedItem || !model) {
      return;
    }
    dispatch(actions.deleteCard(selectedItem.id, selectedItem.groupId));
    setSelectedItem(null);
  };

  const onSubmit = (model: FormModel | null) => {
    if (!selectedItem || !model) {
      return;
    }
    const card: CardSummary = {
      ...selectedItem,
      front: {
        ...selectedItem.front,
        value: model.frontValue,
        example: model.frontExample,
        isTicked: model.isTicked,
        isUsed: model.frontEnabled,
      },
      back: {
        ...selectedItem.back,
        value: model.backValue,
        example: model.backExample,
        isTicked: model.isTicked,
        isUsed: model.backEnabled,
      },
    };
    dispatch(actions.udpateCard(card));
    setSelectedItem(null);
  };

  const onCancel = () => {
    setSelectedItem(null);
  };

  return (
    <div>
      {isSearching && <LoadingSpinner />}
      <Overview overview={overview} />
      <button onClick={() => setPageSize(20)}>20</button>
      <button onClick={() => setPageSize(50)}>50</button>
      <button onClick={() => setPageSize(100)}>100</button>
      <button onClick={clearFilters}>All</button>
      <button onClick={tickedOnly}>Ticked only</button>
      <button onClick={lessonIncludedOnly}>Lesson Included</button>
      <button onClick={waitingOnly}>Lesson Included</button>
      <Pagination
        totalCount={cardsCount}
        pageSize={filter.pageSize}
        onPageChagned={onPageChagned}
        search={filter.searchingTerm}
        onSearchChanged={onSearchChanged}
      />
      {cards.map((item, index) => (
        <Fragment key={index}>
          <Row card={item} onClick={onItemSelected} />
        </Fragment>
      ))}
      <CardDialog
        card={getFormModelFromCardSummary(selectedItem)}
        onHide={onCancel}
        onSubmit={onSubmit}
        onDelete={onDelete}
      />
    </div>
  );
}

function Overview({ overview }: OverviewModel): ReactElement {
  return (
    <div>
      totalCount: <b>{overview.all / 2}</b>
    </div>
  );
}

interface OverviewModel {
  overview: CardsOverview;
}

function getFormModelFromCardSummary(card: CardSummary | null): FormModel | null {
  if (card === null) {
    return null;
  }
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
