import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as selectors from "store/cardsSearch/selectors";
import * as actions from "store/cardsSearch/actions";
import { CardSummary } from "./models/cardSummary";
import { CardsOverview } from "./models/cardsOverview";
import { Pagination } from "common/components/pagination/Pagination";
import { PageChangedEvent } from "common/components/pagination/pageChagnedEvent";

export default function CardsPage(): ReactElement {
  const dispatch = useDispatch();
  const cards = useSelector(selectors.selectCards);
  const filter = useSelector(selectors.selectFilter);
  const isSearching = useSelector(selectors.selectIsSearching);
  const overview = useSelector(selectors.selectOverview);

  useEffect(() => {
    dispatch(actions.getOverview());
    dispatch(actions.search());
  }, [dispatch]);

  const onPageChagned = (event: PageChangedEvent) => {
    dispatch(actions.filterSetPagination(event.currectPage, event.count));
  };

  return (
    <div>
      <Overview overview={overview} />
      <Pagination totalCount={overview.all} onPageChagned={onPageChagned} />
      {cards.map((item, index) => (
        <Row key={index} card={item} />
      ))}
    </div>
  );
}

function Row({ card }: RowModel): ReactElement {
  return (
    <div>
      {card.front.value} - {card.back.value}
    </div>
  );
}
interface RowModel {
  card: CardSummary;
}

function Overview({ overview }: OverviewModel): ReactElement {
  return (
    <div>
      totalCount: <b>{overview.all}</b>
    </div>
  );
}

interface OverviewModel {
  overview: CardsOverview;
}
