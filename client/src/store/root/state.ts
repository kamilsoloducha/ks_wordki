export default interface RootState {
  breadcrumbs: Breadcrumb[];
}

export const initialState: RootState = {
  breadcrumbs: [],
};

export interface Breadcrumb {
  url?: string;
  name: string;
}
