import { ReactElement, useEffect } from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import {
  selectCurrectRepeat,
  selectIsCorrect,
  selectLessonState,
  selectRepeats,
} from "store/lesson/selectors";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { LessonState, LessonStateEnum } from "./models/lessonState";

export default function LessonPage(): ReactElement {
  const questions = useSelector(selectRepeats);
  const lessonState = useSelector(selectLessonState);
  const currectRepeat = useSelector(selectCurrectRepeat);
  const isCorrect = useSelector(selectIsCorrect);
  const state = LessonState.getState(lessonState);
  const history = useHistory();

  useEffect(() => {
    if (lessonState === LessonStateEnum.FinishPending) {
      history.push("lesson-result");
    }
  }, [lessonState, history]);

  useEffect(() => {
    if (questions.length <= 0 && state.type < LessonStateEnum.FinishPending) {
      history.push("");
    }
  }, [questions, history]);

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={state} />
      <Inserting state={state} isCorrect={isCorrect} repeat={currectRepeat} />
      <RepeatsController lessonState={state} />
    </>
  );
}
