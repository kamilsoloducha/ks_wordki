import { Action } from "@reduxjs/toolkit";
import GroupsState, { initialState } from "../state";
import * as actions from "../reducer";

interface Context {
  givenState: GroupsState;
  givenAction: Action;
  expectedState: GroupsState;
}

export class GetGroupsSummary implements Context {
  givenState: GroupsState = { ...initialState, isLoading: false, selectedItem: {} as any };
  givenAction = actions.getGroupsSummary();
  expectedState: GroupsState = { ...initialState, isLoading: true, selectedItem: null };
}

export class GetGroupsSummarySuccess implements Context {
  givenState: GroupsState = { ...initialState, isLoading: true, groups: null as any };
  givenAction = actions.getGroupsSummarySuccess({ groups: [{} as any] });
  expectedState: GroupsState = { ...initialState, isLoading: false, groups: [{} as any] };
}

export class SelectItemById implements Context {
  givenState: GroupsState = { ...initialState, groups: [{ id: "1" } as any, { id: "2" } as any] };
  givenAction = actions.selectItemById({ groupId: "2" });
  expectedState: GroupsState = {
    ...initialState,
    groups: [{ id: "1" } as any, { id: "2" } as any],
    selectedItem: { id: "2" } as any,
  };
}

export class SelectItem implements Context {
  givenState: GroupsState = { ...initialState };
  givenAction = actions.selectItem({ group: { id: "2" } as any });
  expectedState: GroupsState = {
    ...initialState,
    selectedItem: { id: "2" } as any,
  };
}

export class ResetSelectedItem implements Context {
  givenState: GroupsState = { ...initialState, selectedItem: { id: "2" } as any };
  givenAction = actions.resetSelectedItem();
  expectedState: GroupsState = {
    ...initialState,
    selectedItem: null,
  };
}

export class AddGroup implements Context {
  givenState: GroupsState = { ...initialState };
  givenAction = actions.addGroup({ group: {} as any });
  expectedState: GroupsState = { ...initialState };
}

export class AddGroupSuccess implements Context {
  givenState: GroupsState = { ...initialState, selectedItem: {} as any };
  givenAction = actions.addGroupSuccess();
  expectedState: GroupsState = { ...initialState, selectedItem: null };
}

export class UpdateGroup implements Context {
  givenState: GroupsState = { ...initialState };
  givenAction = actions.updateGroup({ group: {} as any });
  expectedState: GroupsState = { ...initialState };
}

export class UpdateGroupSuccess implements Context {
  givenState: GroupsState = { ...initialState, selectedItem: {} as any };
  givenAction = actions.updateGroupSuccess();
  expectedState: GroupsState = { ...initialState, selectedItem: null };
}
