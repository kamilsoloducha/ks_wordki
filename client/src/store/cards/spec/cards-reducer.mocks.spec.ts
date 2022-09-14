import * as actions from "../reducer";
import { Action } from "@reduxjs/toolkit";
import CardsState from "../state";

const initialState: CardsState = {
  isLoading: false,
  id: "",
  name: "",
  language1: 0,
  language2: 0,
  cards: [],
  filteredCards: [],
  selectedItem: null,
  filter: {
    drawer: null,
    isLearning: null,
    text: "",
    isTicked: false,
  },
};

interface Context {
  givenState: CardsState;
  givenAction: Action;
  expectedResult: CardsState;
}

export class GetCards implements Context {
  givenState = {
    ...initialState,
  };
  givenAction = actions.getCards({groupId:"1"});
  expectedResult: CardsState = {
    ...initialState,
    isLoading: true,
  };
}
