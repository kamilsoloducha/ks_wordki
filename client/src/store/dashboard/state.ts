export default interface DashboardState {
  isLoading: boolean;
  dailyRepeats: number;
  groupsCount: number;
  cardsCount: number;
}

export const initailState: DashboardState = {
  isLoading: true,
  dailyRepeats: 0,
  groupsCount: 0,
  cardsCount: 0,
};
