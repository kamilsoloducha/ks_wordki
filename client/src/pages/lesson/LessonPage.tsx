import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getCards, getCardsCount, reset } from "store/lesson/actions";
import {
  selectCardsCount,
  selectCurrectRepeat,
  selectIsCorrect,
  selectLessonState,
  selectRepeats,
} from "store/lesson/selectors";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import Settings from "./components/settings/Settings";
import { LessonState, SetLesson } from "./models/lessonState";

export default function LessonPage(): ReactElement {
  const dispatch = useDispatch();
  const [selectedCount, setSelectedCount] = useState(0);
  const cardsCount = useSelector(selectCardsCount);
  const questions = useSelector(selectRepeats);
  const lessonState = useSelector(selectLessonState);
  const currectRepeat = useSelector(selectCurrectRepeat);
  const isCorrect = useSelector(selectIsCorrect);
  const state = LessonState.getState(lessonState);

  const showSettings = state === SetLesson;

  const selectionChanged = useCallback(
    (value: number) => {
      setSelectedCount(value);
    },
    [setSelectedCount]
  );

  const startLesson = () => {
    dispatch(getCards(selectedCount));
  };

  useEffect(() => {
    if (state === SetLesson) {
      dispatch(getCardsCount());
    }
    return () => {
      dispatch(reset());
    };
  }, []); // eslint-disable-line

  return (
    <>
      {showSettings && (
        <>
          <Settings
            questionCount={cardsCount}
            selectionChanged={selectionChanged}
          />
          <button onClick={startLesson}>Start</button>
        </>
      )}
      {!showSettings && (
        <>
          <div>Pozostało: {questions.length}</div>
          <LessonController lessonState={state} />
          <Inserting
            state={state}
            isCorrect={isCorrect}
            repeat={currectRepeat}
          />
          <RepeatsController lessonState={state} />
        </>
      )}
    </>
  );
}
