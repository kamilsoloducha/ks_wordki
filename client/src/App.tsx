import { lazy, Suspense } from "react";
import { BrowserRouter, Link, Route, Switch } from "react-router-dom";
import "./App.css";
import LoadingPage from "common/components/loadingPage/LoadingPage";
import AxiosEx from "common/components/axiosEx/AxiosEx";
import "primeicons/primeicons.css";
import "primereact/resources/themes/nova/theme.css";
import "primereact/resources/primereact.min.css";
import ErrorPage from "common/components/error/ErrorPage";
import { useDispatch, useSelector } from "react-redux";
import { getLoginUserSuccess } from "store/user/actions";
import { UserData } from "common/models/userModel";
import { selectIsLogin } from "store/user/selectors";

const LoginPage = lazy(() => import("pages/login/LoginPage"));
const LogoutPage = lazy(() => import("pages/logout/LogoutPage"));
const RegisterPage = lazy(() => import("pages/register/RegisterPage"));
const DashboardPage = lazy(() => import("pages/dashboard/DashbaordPage"));
const GroupsPage = lazy(() => import("pages/groups/GroupsPage"));
const GroupDetails = lazy(() => import("pages/cards/GroupDetailsPage"));
const CardsPage = lazy(() => import("pages/cards/CardsPage"));
const LessonPage = lazy(() => import("pages/lesson/LessonPage"));

export default function App() {
  const isLogin = useSelector(selectIsLogin);
  const dispatch = useDispatch();
  const storageValue = localStorage.getItem("user");
  let storageUser: UserData;
  if (storageValue) {
    storageUser = JSON.parse(storageValue);
    dispatch(
      getLoginUserSuccess(storageUser.token, storageUser.id, new Date(1))
    );
  }

  return (
    <>
      <AxiosEx>
        <BrowserRouter>
          <div id="router-container">
            <ul>
              {isLogin && (
                <>
                  <li>
                    <Link to="/logout">Logout</Link>
                  </li>
                  <li>
                    <Link to="/dashboard">Dashboard</Link>
                  </li>
                </>
              )}
              {!isLogin && (
                <>
                  <li>
                    <Link to="/register">Register</Link>
                  </li>
                  <li>
                    <Link to="/login">Login</Link>
                  </li>
                </>
              )}
            </ul>
            <hr />
          </div>
          <div className="content">
            <Suspense fallback={<LoadingPage></LoadingPage>}>
              <Switch>
                <Route path="/logout" component={LogoutPage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/register" component={RegisterPage} />
                <Route path="/dashboard" component={DashboardPage} />
                <Route path="/groups" component={GroupsPage} />
                <Route path="/cards/:groupId" component={GroupDetails} />
                <Route path="/cards" component={CardsPage} />
                <Route path="/error" component={ErrorPage} />
                <Route path="/lesson" component={LessonPage} />
              </Switch>
            </Suspense>
          </div>
        </BrowserRouter>
      </AxiosEx>
    </>
  );
}

export interface UserModel {
  id: string;
  token: string;
}
