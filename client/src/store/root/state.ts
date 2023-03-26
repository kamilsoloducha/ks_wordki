export default interface RootState {
  breadcrumbs: Breadcrumb[];
  cardsSides: string[];
}

export const initialState: RootState = {
  breadcrumbs: [],
  cardsSides: [],
};

export interface Breadcrumb {
  url?: string;
  name: string;
}
