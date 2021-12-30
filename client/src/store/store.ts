import createSagaMiddleware from "@redux-saga/core";
import { createStore, combineReducers, applyMiddleware, compose } from "redux";
import cardsReducer from "./cards/reducer";
import addCardEffect from "./cards/sagas/addCard";
import getCardsEffect from "./cards/sagas/getCards";
import getCardsEffectDaily from "./lesson/sagas/getCards";
import updateCardEffect from "./cards/sagas/updateCard";
import lessonReducer from "./lesson/reducer";
import dashboardReducer from "./dashboard/reducer";
import getDashbaordSummaryEffect from "./dashboard/sagas/getDashboardSummary";
import groupsReducer from "./groups/reducer";
import getGroupsSummaryEffect from "./groups/sagas/getGroupsSummary";
import rootReducer from "./root/reducer";
import userReducer from "./user/reducer";
import loginUserEffect from "./user/sagas/loginUser";
import { correctEffect, wrongEffect } from "./lesson/sagas/answer";
import deleteCardEffect from "./cards/sagas/deleteCard";
import addGroupEffect from "./groups/sagas/addGroup";
import updateGroupEffect from "./groups/sagas/updateGroup";
import getCardsCountEffect from "./lesson/sagas/getCardsCount";
import connectGroupsEffect from "./groups/sagas/connectGroups";
import tickCardEffect from "./lesson/sagas/tickCard";
import appendCardEffect from "./cards/sagas/appendCard";

const sagaMiddleware = createSagaMiddleware();

export function configureStore() {
  const composeEnhancers =
    (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose; // add support for Redux dev tools
  return createStore(
    mainReducer,
    composeEnhancers(applyMiddleware(sagaMiddleware))
  );
}

const mainReducer = combineReducers({
  rootReducer,
  userReducer,
  dashboardReducer,
  groupsReducer,
  cardsReducer,
  lessonReducer,
});

export const store = configureStore();

sagaMiddleware.run(getDashbaordSummaryEffect);
sagaMiddleware.run(getGroupsSummaryEffect);
sagaMiddleware.run(loginUserEffect);
sagaMiddleware.run(getCardsEffect);
sagaMiddleware.run(updateCardEffect);
sagaMiddleware.run(addCardEffect);
sagaMiddleware.run(getCardsEffectDaily);
sagaMiddleware.run(getCardsCountEffect);
sagaMiddleware.run(correctEffect);
sagaMiddleware.run(wrongEffect);
sagaMiddleware.run(deleteCardEffect);
sagaMiddleware.run(addGroupEffect);
sagaMiddleware.run(updateGroupEffect);
sagaMiddleware.run(connectGroupsEffect);
sagaMiddleware.run(tickCardEffect);
sagaMiddleware.run(appendCardEffect);

export type MainState = ReturnType<typeof store.getState>;
