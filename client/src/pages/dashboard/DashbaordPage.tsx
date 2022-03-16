import "./DashboardPage.scss";
import { ReactElement, useEffect } from "react";
import * as router from "react-router-dom";
import { Info } from "./components/info/Info";
import { useDispatch, useSelector } from "react-redux";
import { getDashboardSummary } from "store/dashboard/actions";
import { selectData } from "store/dashboard/selectors";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";
import { Forecast } from "./components/forecast/Forecast";

export default function DashboardPage(): ReactElement {
  const data = useSelector(selectData);
  const dispatch = useDispatch();
  const history = router.useHistory();

  useEffect(() => {
    document.title = "Wordki - Dashboard";
    dispatch(getDashboardSummary());
  }, [dispatch]);

  return (
    <>
      {data.isLoading && <LoadingSpinner />}
      <div id="info-group">
        <Info
          title="Repeats"
          value={data.dailyRepeats}
          onClick={() => history.push("/lesson-settings")}
        />
        <Info title="Groups" value={data.groupsCount} onClick={() => history.push("/groups")} />
        <Info title="Cards" value={data.cardsCount} onClick={() => history.push("/cards")} />
      </div>
      <Forecast />
    </>
  );
}
