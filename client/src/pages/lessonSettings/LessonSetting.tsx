import "./LessonSetting.scss";
import * as act from "store/lesson/actions";
import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { selectIsProcessing, selectSettings } from "store/lesson/selectors";
import Settings from "./components/repetitionSettings/RepetitionSettings";
import { TabView, TabViewItemModel } from "common/components/tabView/TabeView";
import { LessonSettings as SettingsModel } from "pages/lessonSettings/models/lessonSettings";
import NewCardsSettings from "./components/newCardsSettings/NewCardsSettings";
import * as mode from "./models/lesson-mode";
import LoadingSpinner from "common/components/loadingSpinner/LoadingSpinner";

export default function LessonSettings(): ReactElement {
  const settings = useSelector(selectSettings);
  const isProcessing = useSelector(selectIsProcessing);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(act.resetResults());
    dispatch(act.getCardsCount());
  }, [dispatch]);

  const items: TabViewItemModel[] = [
    {
      header: "Repetition",
      element: <Settings />,
      value: mode.Repetition,
    },
    {
      header: "New words",
      element: <NewCardsSettings />,
      value: mode.New,
    },
  ];

  const onModeChanged = (value: number) => {
    dispatch(act.setSettingMode(value + 1));
  };

  return (
    <>
      {isProcessing && <LoadingSpinner />}
      <TabView selectedValue={settings.mode} items={items} onItemChanged={onModeChanged} />
      <div className="settings-container">
        <button
          disabled={!canLessonStart(settings) || isProcessing}
          onClick={() => dispatch(act.getCards())}
        >
          Start
        </button>
      </div>
    </>
  );
}

function canLessonStart(settings: SettingsModel): boolean {
  return settings.count > 0 && settings.type >= 0;
}
