import { Action } from "@reduxjs/toolkit";
import { CardsSearchState } from "../state";
import * as actions from "../reducer";
import { CardsOverview } from "pages/cardsSearch/models";

function initialState(): CardsSearchState {
  return {
    isSearching: false,
    cards: [],
    cardsCount: 0,
    overview: { all: 0, drawers: [0, 0, 0, 0, 0], lessonIncluded: 0, ticked: 0 },
    filter: {
      searchingTerm: "",
      tickedOnly: null,
      lessonIncluded: null,
      pageNumber: 1,
      pageSize: 50,
    },
  };
}

interface Context {
  givenState: CardsSearchState;
  action: Action;
  expectedResult: CardsSearchState;
}

export class GetOverviewCtx implements Context {
  givenState = initialState();
  action = actions.getOverview();
  expectedResult = initialState();
}

export class GetOverviewSuccessCtx implements Context {
  givenState = initialState();
  action = actions.getOverviewSuccess({
    overview: {
      all: 1,
      drawers: [1, 2, 3],
      lessonIncluded: 1,
      ticked: 2,
    } as CardsOverview,
  });
  expectedResult = {
    ...initialState(),
    overview: { all: 1, drawers: [1, 2, 3], lessonIncluded: 1, ticked: 2 },
  } as CardsSearchState;
}

export class SearchCtx implements Context {
  givenState = initialState();
  action = actions.search();
  expectedResult = { ...initialState(), isSearching: true } as CardsSearchState;
}

export class SearchSuccessCtx implements Context {
  givenState = initialState();
  action = actions.searchSuccess({ cards: [{} as any], cardsCount: 5 });
  expectedResult = {
    ...initialState(),
    isSearching: false,
    cards: [{} as any],
    cardsCount: 5,
  } as CardsSearchState;
}

export class FilterResetCtx implements Context {
  givenState: CardsSearchState = { ...initialState(), filter: { lessonIncluded: true } as any };
  action = actions.filterReset();
  expectedResult = initialState();
}

export class FilterSetTermCtx implements Context {
  givenState = initialState();
  action = actions.filterSetTerm({ searchingTerm: "test" });
  expectedResult: CardsSearchState = {
    ...initialState(),
    filter: { ...initialState().filter, searchingTerm: "test" },
  };
}

export class FilterSetTickedCtx implements Context {
  givenState = initialState();
  action = actions.filterSetTickedOnly({ tickedOnly: true });
  expectedResult: CardsSearchState = {
    ...initialState(),
    filter: { ...initialState().filter, tickedOnly: true },
  };
}

export class FilterSetLessonIncludedCtx implements Context {
  givenState = initialState();
  action = actions.filterSetLessonIncluded({ lessonIncluded: true });
  expectedResult: CardsSearchState = {
    ...initialState(),
    filter: { ...initialState().filter, lessonIncluded: true },
  };
}

export class FilterSetPaginationCtx implements Context {
  givenState = initialState();
  action = actions.filterSetPagination({ pageNumber: 1, pageSize: 2 });
  expectedResult: CardsSearchState = {
    ...initialState(),
    filter: { ...initialState().filter, pageNumber: 1, pageSize: 2 },
  };
}

export class UpdateCardCtx implements Context {
  givenState = initialState();
  action = actions.updateCard({ card: {} as any });
  expectedResult = initialState();
}

export class UpdateCardSuccessCtx implements Context {
  givenState = initialState();
  action = actions.updateCardSuccess({ card: {} as any });
  expectedResult = initialState();
}

export class DeleteCardCtx implements Context {
  givenState = initialState();
  action = actions.deleteCard({ cardId: "1", groupId: "2" });
  expectedResult = initialState();
}
