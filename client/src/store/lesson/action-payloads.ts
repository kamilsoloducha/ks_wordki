import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { Repeat } from "pages/lesson/models/repeat";
import { Group } from "pages/lessonSettings/models/group";
import { Language } from "pages/lessonSettings/models/languages";

export interface SetSettingCount {
  count: number;
}

export interface SetSettingLanguage {
  languages: string[];
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

export interface GetLanguagesSuccess {
  languages: Language[];
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
