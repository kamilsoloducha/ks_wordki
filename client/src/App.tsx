import "./App.scss";
import "primeicons/primeicons.css";
import "primereact/resources/themes/nova/theme.css";
import "primereact/resources/primereact.min.css";
import { lazy, Suspense } from "react";
import { Route, Routes } from "react-router-dom";
import AxiosEx from "common/components/axiosEx/AxiosEx";
import { selectIsLogin } from "store/user/selectors";
import TopBar from "common/components/topBar/TopBar";
import { selectBreadcrumbs } from "store/root/selectors";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import { useAppDispatch, useAppSelector } from "store/store";
import { loginSuccess } from "store/user/reducer";
import { CustomRouter } from "common/components/CustomRouter";
import history from "common/services/history";

const LoginPage = lazy(() => import("pages/login/LoginPage"));
const LogoutPage = lazy(() => import("pages/logout/LogoutPage"));
const RegisterPage = lazy(() => import("pages/register/RegisterPage"));
const DashboardPage = lazy(() => import("pages/dashboard/DashbaordPage"));
const GroupsPage = lazy(() => import("pages/groups/GroupsPage"));
const GroupsSearchPage = lazy(() => import("pages/groupsSearch/GroupsSearch"));
const GroupDetails = lazy(() => import("pages/cards/GroupDetailsPage"));
const CardsPage = lazy(() => import("pages/cardsSearch/CardsPage"));
const LessonSettingsPage = lazy(() => import("pages/lessonSettings/LessonSetting"));
const LessonPage = lazy(() => import("pages/lesson/LessonPage"));
const LessonResultPage = lazy(() => import("pages/lessonResult/LessonResult"));
const ErrorPage = lazy(() => import("pages/error/ErrorPage"));

export default function App() {
  const dispatch = useAppDispatch();

  const isLogin = useAppSelector(selectIsLogin);
  const breadCrumbs = useAppSelector(selectBreadcrumbs);

  const userId = localStorage.getItem("id");
  const token = localStorage.getItem("token");

  if (userId && token) {
    dispatch(loginSuccess({ id: userId, token, expirationDate: "2022/12/12" }));
  }

  return (
    <>
      <AxiosEx>
        <CustomRouter history={history}>
          <TopBar isLogin={isLogin} breadCrumbs={breadCrumbs} />
          <div className="content">
            <Suspense fallback={<LoadingSpinner />}>
              <Routes>
                <Route path="/logout" element={<LogoutPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/dashboard" element={<DashboardPage />} />
                <Route path="/groups/search" element={<GroupsSearchPage />} />
                <Route path="/groups" element={<GroupsPage />} />
                <Route path="/cards/:groupId" element={<GroupDetails />} />
                <Route path="/cards" element={<CardsPage />} />
                <Route path="/error" element={<ErrorPage />} />
                <Route path="/lesson-settings" element={<LessonSettingsPage />} />
                <Route path="/lesson-result" element={<LessonResultPage />} />
                <Route path="/lesson" element={<LessonPage />} />
                <Route path="/" element={<DashboardPage />} />
                <Route path="/error" element={<ErrorPage />} />
              </Routes>
            </Suspense>
          </div>
        </CustomRouter>
      </AxiosEx>
    </>
  );
}
