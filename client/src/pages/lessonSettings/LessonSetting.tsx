import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import * as act from "store/lesson/actions";
import {
  selectCardsCount,
  selectRepeats,
  selectSettings,
} from "store/lesson/selectors";
import Settings from "./components/settings/Settings";

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

  return (
    <>
      <Settings
        settings={settings}
        questionCount={cardsCount ? cardsCount : 0}
        countChanged={(value: number) => dispatch(act.setSettingCount(value))}
        languageChanged={(value: number) =>
          dispatch(act.setSettingLanguage(value))
        }
        typeChanged={(value: number) => dispatch(act.setSettingType(value))}
      />
      <button onClick={startLesson}>Start</button>
    </>
  );
}
