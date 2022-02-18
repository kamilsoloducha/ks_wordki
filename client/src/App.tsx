import { lazy, Suspense } from "react";
import { Route, Router, Switch } from "react-router-dom";
import "./App.css";
import LoadingPage from "common/components/loadingPage/LoadingPage";
import AxiosEx from "common/components/axiosEx/AxiosEx";
import "primeicons/primeicons.css";
import "primereact/resources/themes/nova/theme.css";
import "primereact/resources/primereact.min.css";
import ErrorPage from "common/components/error/ErrorPage";
import { useDispatch, useSelector } from "react-redux";
import { getLoginUserSuccess } from "store/user/actions";
import { selectIsLogin } from "store/user/selectors";
import TopBar from "common/components/topBar/TopBar";
import GuardedRoute from "common/components/guardedRoute/GuardedRoute";
import history from "./common/services/history";

const LoginPage = lazy(() => import("pages/login/LoginPage"));
const LogoutPage = lazy(() => import("pages/logout/LogoutPage"));
const RegisterPage = lazy(() => import("pages/register/RegisterPage"));
const DashboardPage = lazy(() => import("pages/dashboard/DashbaordPage"));
const GroupsPage = lazy(() => import("pages/groups/GroupsPage"));
const GroupDetails = lazy(() => import("pages/cards/GroupDetailsPage"));
const CardsPage = lazy(() => import("pages/cards/CardsPage"));
const LessonSettingsPage = lazy(() => import("pages/lessonSettings/LessonSetting"));
const LessonPage = lazy(() => import("pages/lesson/LessonPage"));
const LessonResultPage = lazy(() => import("pages/lessonResult/LessonResult"));

export default function App() {
  const isLogin = useSelector(selectIsLogin);
  const dispatch = useDispatch();
  const userId = localStorage.getItem("id");
  const token = localStorage.getItem("token");
  if (userId && token) {
    dispatch(getLoginUserSuccess(token, userId, new Date(1)));
  }

  return (
    <>
      <AxiosEx>
        <Router history={history}>
          <TopBar isLogin={isLogin} />
          <div className="content">
            <Suspense fallback={<LoadingPage></LoadingPage>}>
              <Switch>
                <Route path="/logout" component={LogoutPage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/register" component={RegisterPage} />
                <GuardedRoute path="/dashboard" component={DashboardPage} auth={isLogin} />
                <GuardedRoute path="/groups" component={GroupsPage} auth={isLogin} />
                <GuardedRoute path="/cards/:groupId" component={GroupDetails} auth={isLogin} />
                <GuardedRoute path="/cards" component={CardsPage} auth={isLogin} />
                <Route path="/error" component={ErrorPage} />
                <GuardedRoute
                  path="/lesson-settings"
                  component={LessonSettingsPage}
                  auth={isLogin}
                />
                <GuardedRoute path="/lesson-result" component={LessonResultPage} auth={isLogin} />
                <GuardedRoute path="/lesson" component={LessonPage} auth={isLogin} />
                <GuardedRoute path="/" component={DashboardPage} auth={isLogin} />
              </Switch>
            </Suspense>
          </div>
        </Router>
      </AxiosEx>
    </>
  );
}

export interface UserModel {
  id: string;
  token: string;
}
