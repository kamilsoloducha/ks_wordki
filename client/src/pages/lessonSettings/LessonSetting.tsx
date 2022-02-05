import * as act from "store/lesson/actions";
import * as mode from "./models/lesson-mode";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import {
  selectCardsCount,
  selectRepeats,
  selectSettings,
} from "store/lesson/selectors";
import Settings from "./components/settings/Settings";
import NewSettings from "./components/newSettings/newSettings";

export default function LessonSettings(): ReactElement {
  const cardsCount = useSelector(selectCardsCount);
  const settings = useSelector(selectSettings);
  const repeats = useSelector(selectRepeats);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    dispatch(act.getCardsCount());
  }, [dispatch]);

  const startLesson = useCallback(() => {
    dispatch(act.getCards());
  }, [dispatch]);

  useEffect(() => {
    if (repeats.length > 0) {
      history.push("lesson");
    }
  }, [repeats, history]);

  const onModeChanged = useCallback(
    (event$: any) => {
      const value = parseInt(event$.target.value);
      dispatch(act.setSettingMode(value));
    },
    [dispatch]
  );

  return (
    <>
      <form>
        <input
          type="radio"
          name="mode"
          id="repetition"
          value={mode.Repetition}
          onChange={onModeChanged}
          checked={settings.mode === mode.Repetition}
        />
        <label htmlFor="repetition">Repetition</label> <br />
        <input
          type="radio"
          name="mode"
          id="new"
          value={mode.New}
          onChange={onModeChanged}
          checked={settings.mode === mode.New}
        />
        <label htmlFor="new">New words</label>
      </form>
      {settings.mode === mode.Repetition ? (
        <Settings
          settings={settings}
          questionCount={cardsCount ? cardsCount : 0}
          countChanged={(value: number) => dispatch(act.setSettingCount(value))}
          languageChanged={(value: number) =>
            dispatch(act.setSettingLanguage(value))
          }
          typeChanged={(value: number) => dispatch(act.setSettingType(value))}
        />
      ) : (
        <NewSettings />
      )}
      <button onClick={startLesson}>Start</button>
    </>
  );
}
