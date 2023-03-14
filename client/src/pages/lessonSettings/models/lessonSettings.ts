import { Group } from "./group";

export interface LessonSettings {
  type: number;
  languages: string[];
  count: number;
  mode: number;
  groups: Group[];
  selectedGroupId: string | null;
  wrongLimit: number;
}
