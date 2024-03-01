import React, { lazy } from 'react'
import ReactDOM from 'react-dom/client'
import './index.scss'
import { Provider } from 'react-redux'
import { store } from 'store/store'
import TestPage from 'pages/test/TestPage'
import Axios from 'common/components/AxiosEx'
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements
} from 'react-router-dom'
import { Root } from 'pages/Root'
import LoadingSpinner from 'common/components/LoadingSpinner'
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

const createProtectedRoute = (
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
    {createProtectedRoute('/login', false, true, <LoginPage />)}
    {createProtectedRoute('/register', false, true, <RegisterPage />)}
    {createProtectedRoute('/dashboard', true, false, <DashboardPage />)}
    {createProtectedRoute('/', true, false, <DashboardPage />)}
    {createProtectedRoute('/error', true, false, <ErrorPage />)}
    {createProtectedRoute('/lesson', true, false, <LessonPage />)}
    {createProtectedRoute('/lesson-settings', true, false, <LessonSettingsPage />)}
    {createProtectedRoute('/lesson-result', true, false, <LessonResultPage />)}
    {createProtectedRoute('/groups', true, false, <GroupsPage />)}
    {createProtectedRoute('/groups/search', true, false, <GroupsSearchPage />)}
    {createProtectedRoute('/cards/:groupId', true, false, <GroupDetails />)}
    {createProtectedRoute('/cards', true, false, <CardsPage />)}
    {createProtectedRoute('/test', true, false, <TestPage />)}
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
