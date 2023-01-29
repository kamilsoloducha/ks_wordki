import createSagaMiddleware from "@redux-saga/core";
import cardsReducer from "./cards/reducer";
import lessonReducer from "./lesson/reducer";
import dashboardReducer from "./dashboard/reducer";
import groupsReducer from "./groups/reducer";
import rootReducer from "./root/reducer";
import userReducer from "./user/reducer";
import cardsSeachReducer from "./cardsSearch/reducer";
import groupsSearchReducer from "./groupsSearch/reducer";
import * as cardsSearch from "./cardsSearch/saga";
import * as cards from "./cards/sagas";
import * as dashboard from "./dashboard/sagas";
import * as groups from "./groups/sagas";
import * as groupsSearch from "./groupsSearch/sagas";
import * as lesson from "./lesson/sagas";
import * as user from "./user/sagas";
import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

const sagaMiddleware = createSagaMiddleware();

export const store = configureStore({
  reducer: {
    userReducer,
    cardsReducer,
    lessonReducer,
    dashboardReducer,
    groupsReducer,
    rootReducer,
    cardsSeachReducer,
    groupsSearchReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(sagaMiddleware),
});

sagaMiddleware.run(user.loginUserEffect);
sagaMiddleware.run(user.registerUserEffect);

sagaMiddleware.run(dashboard.getDashbaordSummaryEffect);

sagaMiddleware.run(cards.getCardsEffect);
sagaMiddleware.run(cards.updateCardEffect);
sagaMiddleware.run(cards.addCardEffect);
sagaMiddleware.run(cards.deleteCardEffect);
sagaMiddleware.run(cards.setFilterEffect);
sagaMiddleware.run(cards.getCardEffect);

sagaMiddleware.run(groups.getGroupsSummaryEffect);
sagaMiddleware.run(groups.addGroupEffect);
sagaMiddleware.run(groups.updateGroupEffect);

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

sagaMiddleware.run(groupsSearch.getCardsEffect);
sagaMiddleware.run(groupsSearch.saveGroupEffect);
sagaMiddleware.run(groupsSearch.searchEffect);
sagaMiddleware.run(groupsSearch.setGroupEffect);

export type MainState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  MainState,
  unknown,
  Action<string>
>;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<MainState> = useSelector;
