import React, { lazy } from 'react'
import ReactDOM from 'react-dom/client'
import './index.scss'
import { Provider } from 'react-redux'
import { store } from 'store/store'
import TestPage from 'pages/test/TestPage'
import Axios from 'common/components/axiosEx/AxiosEx'
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements
} from 'react-router-dom'
import { Root } from 'pages/Root'
import LoadingSpinner from 'components/loadingSpinner/LoadingSpinner'
import ProtectedRoute from 'common/components/ProtectedRoute'

const LoginPage = lazy(() => import('pages/login/LoginPage'))
const LogoutPage = lazy(() => import('pages/logout/LogoutPage'))
const RegisterPage = lazy(() => import('pages/register/RegisterPage'))
const DashboardPage = lazy(() => import('pages/dashboard/DashbaordPage'))
const GroupsPage = lazy(() => import('pages/groups/GroupsPage'))
const GroupsSearchPage = lazy(() => import('pages/groupsSearch/GroupsSearch'))
const GroupDetails = lazy(() => import('pages/cards/GroupDetailsPage'))
const CardsPage = lazy(() => import('pages/cardsSearch/CardsPage'))
const LessonSettingsPage = lazy(() => import('pages/lessonSettings/LessonSetting'))
const LessonPage = lazy(() => import('pages/lesson/LessonPage'))
const LessonResultPage = lazy(() => import('pages/lessonResult/LessonResult'))
const ErrorPage = lazy(() => import('pages/error/ErrorPage'))

const createRoute = (
  path: string,
  isLoginRequired: boolean,
  isLoginForbiden: boolean,
  page: React.ReactElement
) => {
  return (
    <Route
      path={path}
      element={
        <React.Suspense fallback={<LoadingSpinner />}>
          <ProtectedRoute isLoginRequired={isLoginRequired} isLoginForbiden={isLoginForbiden}>
            {page}
          </ProtectedRoute>
        </React.Suspense>
      }
    />
  )
}

const routes = createRoutesFromElements(
  <Route
    path="/"
    element={
      <Axios>
        <Root />
      </Axios>
    }
  >
    {createRoute('/login', false, true, <LoginPage />)}
    {createRoute('/register', false, true, <RegisterPage />)}
    {createRoute('/dashboard', true, false, <DashboardPage />)}
    {createRoute('/', true, false, <DashboardPage />)}
    {createRoute('/error', true, false, <ErrorPage />)}
    {createRoute('/lesson', true, false, <LessonPage />)}
    {createRoute('/lesson-settings', true, false, <LessonSettingsPage />)}
    {createRoute('/lesson-result', true, false, <LessonResultPage />)}
    {createRoute('/groups', true, false, <GroupsPage />)}
    {createRoute('/groups/search', true, false, <GroupsSearchPage />)}
    {createRoute('/cards/:groupId', true, false, <GroupDetails />)}
    {createRoute('/cards', true, false, <CardsPage />)}
    {createRoute('/test', true, false, <TestPage />)}
    <Route
      path="/logout"
      element={
        <React.Suspense fallback={<LoadingSpinner />}>
          <LogoutPage />
        </React.Suspense>
      }
    />
  </Route>
)

const router = createBrowserRouter(routes)

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
)
