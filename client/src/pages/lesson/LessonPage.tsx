import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getCards, reset } from "store/lesson/actions";
import {
  selectCurrectRepeat,
  selectLessonState,
  selectRepeats,
} from "store/lesson/selectors";
import Fiszka from "./components/fiszka/Fiszka";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { BeforeLoading, LessonState } from "./models/lessonState";

function DailyPage(): ReactElement {
  const dispatch = useDispatch();
  const questions = useSelector(selectRepeats);
  const lessonState = useSelector(selectLessonState);
  const currectRepeat = useSelector(selectCurrectRepeat);
  const state = LessonState.getState(lessonState);

  useEffect(() => {
    if (state === BeforeLoading) {
      dispatch(getCards(10));
    }
    return () => {
      dispatch(reset());
    };
  }, []);

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={state} />
      <Fiszka lessonState={state} repeat={currectRepeat} />
      <RepeatsController lessonState={state} />
    </>
  );
}

export default DailyPage;
