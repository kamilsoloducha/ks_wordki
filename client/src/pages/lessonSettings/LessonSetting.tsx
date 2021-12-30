import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import * as act from "store/lesson/actions";
import { selectCardsCount, selectRepeats } from "store/lesson/selectors";
import ExtendLesson from "./components/extendLesson/ExtendLesson";
import Settings, { LessonSettingsForm } from "./components/settings/Settings";

export default function LessonSettings(): ReactElement {
  const [lessonSettingsForm, setLessonSettingsForm] =
    useState<LessonSettingsForm>();
  const [source, setSource] = useState<string>("repeat");
  const cardsCount = useSelector(selectCardsCount);
  const repeats = useSelector(selectRepeats);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    if (!lessonSettingsForm) return;
    dispatch(act.getCardsCount(lessonSettingsForm.languageType));
  }, [lessonSettingsForm?.languageType]);

  const selectionChanged = useCallback(
    (value: LessonSettingsForm) => {
      setLessonSettingsForm(value);
    },
    [setLessonSettingsForm]
  );

  const startLesson = useCallback(() => {
    if (!lessonSettingsForm) return;
    dispatch(act.setSettings(lessonSettingsForm.lessonType));
    dispatch(
      act.getCards(lessonSettingsForm.count, lessonSettingsForm.languageType)
    );
  }, [lessonSettingsForm]);

  const onSourceChanged = useCallback((value: string) => {
    setSource(value);
  }, []);

  useEffect(() => {}, []); // eslint-disable-line

  useEffect(() => {
    if (repeats.length > 0) {
      history.push("lesson");
    }
  }, [repeats, history]);

  return (
    <>
      {/* <form>
        <input
          type="radio"
          name="cardsSource"
          id="langPol"
          value="repeat"
          checked={source === "repeat"}
          onChange={() => onSourceChanged("repeat")}
        />
        <label htmlFor="langPol">Repeat</label>
        <input
          type="radio"
          name="cardsSource"
          id="langEng"
          value="new"
          checked={source === "new"}
          onChange={() => onSourceChanged("new")}
        />
        <label htmlFor="langEng">New Words</label>
      </form> */}

      <Settings
        questionCount={cardsCount}
        selectionChanged={selectionChanged}
      />
      {/* <ExtendLesson
          questionCount={cardsCount}
          selectionChanged={selectionChanged}
        /> */}
      <button onClick={startLesson}>Start</button>
    </>
  );
}
