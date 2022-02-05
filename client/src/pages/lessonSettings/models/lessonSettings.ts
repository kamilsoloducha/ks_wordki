import { Group } from "./group";

export interface LessonSettings {
  type: number;
  language: number;
  count: number;
  mode: number;
  groups: Group[];
  selectedGroup: Group;
  wrongLimit: number;
}
