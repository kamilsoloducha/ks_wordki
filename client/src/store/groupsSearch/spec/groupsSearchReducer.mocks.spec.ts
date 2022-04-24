import { Action } from "@reduxjs/toolkit";
import { GroupsSearchState, initialGroupsSearchState } from "../state";
import * as actions from "../reducer";

interface Context {
  givenState: GroupsSearchState;
  givenAction: Action;
  expectedResult: GroupsSearchState;
}

export class Search implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState };
  givenAction = actions.search();
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, isSearching: true };
}

export class SearchSuccess implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState, isSearching: true };
  givenAction = actions.searchSuccess({ groupsCount: 10, groups: [{} as any] });
  expectedResult: GroupsSearchState = {
    ...initialGroupsSearchState,
    isSearching: false,
    groupsCount: 10,
    groups: [{} as any],
  };
}

export class FiterSetName implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState };
  givenAction = actions.filterSetName({ name: "name" });
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, groupName: "name" };
}

export class SetGroup implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState, selectedGroup: null };
  givenAction = actions.setGroup({ group: { id: "id" } as any });
  expectedResult: GroupsSearchState = {
    ...initialGroupsSearchState,
    selectedGroup: { id: "id" } as any,
  };
}

export class ResetSelection implements Context {
  givenState: GroupsSearchState = {
    ...initialGroupsSearchState,
    selectedGroup: { id: "id" } as any,
  };
  givenAction = actions.resetSelection();
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, selectedGroup: null };
}

export class GetCards implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState };
  givenAction = actions.getCards();
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, isCardsLoading: true };
}

export class GetCardsSuccess implements Context {
  givenState: GroupsSearchState = {
    ...initialGroupsSearchState,
    isCardsLoading: true,
    cards: null as any,
  };
  givenAction = actions.getCardsSuccess({ cards: [] });
  expectedResult: GroupsSearchState = {
    ...initialGroupsSearchState,
    isCardsLoading: false,
    cards: [],
  };
}

export class SaveGroup implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState };
  givenAction = actions.saveGroup();
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, isCardsLoading: true };
}

export class SaveGroupSuccess implements Context {
  givenState: GroupsSearchState = { ...initialGroupsSearchState, isCardsLoading: true };
  givenAction = actions.saveGroupSuccess();
  expectedResult: GroupsSearchState = { ...initialGroupsSearchState, isCardsLoading: false };
}
