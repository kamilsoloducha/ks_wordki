import { Group } from "./group";

export interface LessonSettings {
  type: number;
  languages: number[];
  count: number;
  mode: number;
  groups: Group[];
  selectedGroup: Group;
  wrongLimit: number;
}
