import { CardSummaryBuilder, SideSummaryBuilder } from "../../../../test/builders";
import { CardSummary } from "../../models";

interface DrawerCardsCounter {
  cards: CardSummary[];
  drawer: number;
  result: number;
}

export class EmptyList implements DrawerCardsCounter {
  cards = [];
  drawer = 1;
  result = 0;
}

export class SingleItemSingleSideList implements DrawerCardsCounter {
  cards = [
    new CardSummaryBuilder().withFront(new SideSummaryBuilder().withDrawer(5).build()).build(),
  ];
  drawer = 5;
  result = 1;
}

export class SingleItemDoubleSideList implements DrawerCardsCounter {
  cards = [
    new CardSummaryBuilder()
      .withFront(new SideSummaryBuilder().withDrawer(5).build())
      .withBack(new SideSummaryBuilder().withDrawer(5).build())
      .build(),
  ];
  drawer = 5;
  result = 2;
}

export class MultipleItemSingleSideList implements DrawerCardsCounter {
  cards = [
    new CardSummaryBuilder().withFront(new SideSummaryBuilder().withDrawer(5).build()).build(),
    new CardSummaryBuilder().withBack(new SideSummaryBuilder().withDrawer(5).build()).build(),
  ];
  drawer = 5;
  result = 2;
}
