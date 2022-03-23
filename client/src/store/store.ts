import createSagaMiddleware from "@redux-saga/core";
import { createStore, combineReducers, applyMiddleware, compose } from "redux";
import cardsReducer from "./cards/reducer";
import lessonReducer from "./lesson/reducer";
import dashboardReducer from "./dashboard/reducer";
import groupsReducer from "./groups/reducer";
import rootReducer from "./root/reducer";
import userReducer from "./user/reducer";
import cardsSeachReducer from "./cardsSearch/reducer";
import { groupsSearchReducer } from "./groupsSearch/reducer";
import * as cardsSearch from "./cardsSearch/saga";
import * as cards from "./cards/sagas";
import * as dashboard from "./dashboard/sagas";
import * as groups from "./groups/sagas";
import * as groupsSearch from "./groupsSearch/sagas";
import * as lesson from "./lesson/sagas";
import * as user from "./user/sagas";

const sagaMiddleware = createSagaMiddleware();

export function configureStore() {
  const composeEnhancers = (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose; // add support for Redux dev tools
  return createStore(mainReducer, composeEnhancers(applyMiddleware(sagaMiddleware)));
}

const mainReducer = combineReducers({
  rootReducer,
  userReducer,
  dashboardReducer,
  groupsReducer,
  cardsReducer,
  lessonReducer,
  cardsSeachReducer,
  groupsSearchReducer,
});

export const store = configureStore();

sagaMiddleware.run(user.loginUserEffect);
sagaMiddleware.run(user.registerUserEffect);

sagaMiddleware.run(dashboard.getDashbaordSummaryEffect);

sagaMiddleware.run(cards.getCardsEffect);
sagaMiddleware.run(cards.updateCardEffect);
sagaMiddleware.run(cards.addCardEffect);
sagaMiddleware.run(cards.deleteCardEffect);
sagaMiddleware.run(cards.appendCardEffect);
sagaMiddleware.run(cards.setFilterEffect);

sagaMiddleware.run(groups.getGroupsSummaryEffect);
sagaMiddleware.run(groups.addGroupEffect);
sagaMiddleware.run(groups.updateGroupEffect);
sagaMiddleware.run(groups.connectGroupsEffect);
sagaMiddleware.run(groups.searchGroupEffect);

sagaMiddleware.run(lesson.getCardsEffect);
sagaMiddleware.run(lesson.correctEffect);
sagaMiddleware.run(lesson.wrongEffect);
sagaMiddleware.run(lesson.getCardsCountEffect);
sagaMiddleware.run(lesson.tickCardEffect);
sagaMiddleware.run(lesson.setSettingLanguageEffect);
sagaMiddleware.run(lesson.getGroupsEffect);
sagaMiddleware.run(lesson.updateCardEffect);

sagaMiddleware.run(cardsSearch.searchEffect);
sagaMiddleware.run(cardsSearch.getOverviewEffect);
sagaMiddleware.run(cardsSearch.setPaginationEffect);
sagaMiddleware.run(cardsSearch.setSearchingTermEffect);
sagaMiddleware.run(cardsSearch.updateCardEffect);
sagaMiddleware.run(cardsSearch.deleteCardEffect);

sagaMiddleware.run(groupsSearch.searchEffect);

export type MainState = ReturnType<typeof store.getState>;
