import "./DashboardPage.scss";
import { ReactElement, useEffect } from "react";
import { Redirect, useHistory } from "react-router";
import Info from "./components/info/Info";
import { useDispatch, useSelector } from "react-redux";
import { getDashboardSummary } from "store/dashboard/actions";
import { selectData } from "store/dashboard/selectors";
import { selectUserId } from "store/user/selectors";

function DashboardPage(): ReactElement {
  const userId = useSelector(selectUserId);
  const data = useSelector(selectData);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    dispatch(getDashboardSummary());
  }, [dispatch]);

  if (!userId) {
    return <Redirect to="login" />;
  }

  if (data.isLoading) {
    return <>Loading...</>;
  }

  const navigateGroups = () => {
    history.push("/groups");
  };

  const navigateCards = () => {
    history.push("/cards");
  };

  const navigateLesson = () => {
    history.push("/lesson");
  };

  return (
    <>
      <div id="info-group">
        <Info
          title="Powtórzenia"
          value={data.dailyRepeats}
          onClick={navigateLesson}
        />
        <Info title="Grupy" value={data.groupsCount} onClick={navigateGroups} />
        <Info title="Karty" value={data.cardsCount} onClick={navigateCards} />
      </div>
    </>
  );
}

export default DashboardPage;
