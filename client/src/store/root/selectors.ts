import { MainState } from 'store/store'

export const selectBreadcrumbs = (state: MainState) => state.rootReducer.breadcrumbs
