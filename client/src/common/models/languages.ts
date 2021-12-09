export const DEFAULT = 0;
export const POLISH = 1;
export const ENGLISH = 2;

export const Default = {
  type: DEFAULT,
  label: "Default",
  icon: "/flags/undefined.svg",
} as Language;

export const Polish = {
  type: POLISH,
  label: "Polish",
  icon: "/flags/polish.svg",
} as Language;

export const English = {
  type: ENGLISH,
  label: "English",
  icon: "/flags/english.svg",
} as Language;

export const Languages: Language[] = [Default, Polish, English];

export default interface Language {
  type: number;
  label: string;
  icon: string;
}

export function getLanguageByType(type: number): Language {
  switch (type) {
    case 0:
      return Default;
    case 1:
      return Polish;
    case 2:
      return English;
    default:
      return Default;
  }
}
