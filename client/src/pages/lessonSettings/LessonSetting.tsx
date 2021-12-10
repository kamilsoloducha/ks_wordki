import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import * as act from "store/lesson/actions";
import { selectCardsCount, selectRepeats } from "store/lesson/selectors";
import Settings, { LessonSettingsForm } from "./components/settings/Settings";

export default function LessonSettings(): ReactElement {
  const [lessonSettingsForm, setLessonSettingsForm] =
    useState<LessonSettingsForm>();
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

  useEffect(() => {}, []); // eslint-disable-line

  useEffect(() => {
    if (repeats.length > 0) {
      history.push("lesson");
    }
  }, [repeats, history]);

  return (
    <>
      <Settings
        questionCount={cardsCount}
        selectionChanged={selectionChanged}
      />
      <button onClick={startLesson}>Start</button>
    </>
  );
}
