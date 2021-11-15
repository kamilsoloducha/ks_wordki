import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getCards, reset } from "store/lesson/actions";
import {
  selectCurrectRepeat,
  selectIsCorrect,
  selectLessonState,
  selectRepeats,
} from "store/lesson/selectors";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { BeforeLoading, LessonState } from "./models/lessonState";

function LessonPage(): ReactElement {
  const dispatch = useDispatch();
  const questions = useSelector(selectRepeats);
  const lessonState = useSelector(selectLessonState);
  const currectRepeat = useSelector(selectCurrectRepeat);
  const isCorrect = useSelector(selectIsCorrect);
  const state = LessonState.getState(lessonState);

  useEffect(() => {
    if (state === BeforeLoading) {
      dispatch(getCards(10));
    }
    return () => {
      dispatch(reset());
    };
  }, []); // eslint-disable-line

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={state} />
      <Inserting state={state} isCorrect={isCorrect} repeat={currectRepeat} />
      <RepeatsController lessonState={state} />
    </>
  );
}

export default LessonPage;
