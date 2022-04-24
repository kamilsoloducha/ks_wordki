import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { Repeat } from "pages/lesson/models/repeat";
import { Group } from "pages/lessonSettings/models/group";

export interface SetSettingCount {
  count: number;
}

export interface SetSettingLanguage {
  languages: number[];
}

export interface SetSettingType {
  type: number;
}

export interface SetSettingMode {
  mode: number;
}

export interface SetSettingGroup {
  groupId: string;
}

export interface GetCardsSuccess {
  repeats: Repeat[];
}

export interface GetGroupsSuccess {
  groups: Group[];
}

export interface GetCardsCountSuccess {
  count: number;
}

export interface Correct {
  result: number;
}

export interface Wrong {
  result: number;
}

export interface SetAnswer {
  answer: string;
}

export interface UpdateCard {
  form: FormModel;
  groupId: string;
}

export interface UpdateCardSuccess {
  form: FormModel;
}
