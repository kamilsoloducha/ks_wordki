import { Group } from "./group";

export interface LessonSettings {
  type: number;
  languages: number[];
  count: number;
  mode: number;
  groups: Group[];
  selectedGroupId: string | null;
  wrongLimit: number;
}
