export interface Model {
  groupsCount: number;
  cardsCount: number;
  dailyRepeats: number;
  isLoading: boolean;
}

export const initialState: Model = {
  groupsCount: 0,
  cardsCount: 0,
  dailyRepeats: 0,
  isLoading: false,
};
