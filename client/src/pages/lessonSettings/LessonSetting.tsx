import "./LessonSetting.scss";
import * as act from "store/lesson/actions";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { selectRepeats, selectSettings } from "store/lesson/selectors";
import Settings from "./components/repetitionSettings/RepetitionSettings";
import { TabView, TabViewItemModel } from "common/components/tabView/TabeView";
import { LessonSettings as SettingsModel } from "pages/lessonSettings/models/lessonSettings";
import NewCardsSettings from "./components/newCardsSettings/NewCardsSettings";

export default function LessonSettings(): ReactElement {
  const settings = useSelector(selectSettings);
  const repeats = useSelector(selectRepeats);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    dispatch(act.getCardsCount());
  }, [dispatch]);

  useEffect(() => {
    if (repeats.length > 0) {
      history.push("lesson");
    }
  }, [repeats, history]);

  const items: TabViewItemModel[] = [
    {
      header: "Repetition",
      element: <Settings />,
    },
    {
      header: "New words",
      element: <NewCardsSettings />,
    },
  ];

  const onModeChanged = useCallback(
    (value: number) => {
      dispatch(act.setSettingMode(value + 1));
    },
    [dispatch]
  );

  return (
    <>
      <TabView items={items} onItemChanged={onModeChanged} />
      <div className="settings-container">
        <button disabled={!canLessonStart(settings)} onClick={() => dispatch(act.getCards())}>
          Start
        </button>
      </div>
    </>
  );
}

function canLessonStart(settings: SettingsModel): boolean {
  return settings.count > 0 && settings.type >= 0;
}
