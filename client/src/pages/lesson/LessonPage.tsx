import * as actions from "store/lesson/actions";
import * as sel from "store/lesson/selectors";
import * as type from "./models/resultTypes";
import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import Fiszka from "./components/fiszka/Fiszka";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { FinishPending } from "./models/lessonState";
import { RepeatHistory } from "./components/repeatsHistory/RepeatsHistory";

export default function LessonPage(): ReactElement {
  const [repeatsPopup, setRepeatsPopup] = useState(false);
  const questions = useSelector(sel.selectRepeats);
  const status = useSelector(sel.selectLessonState);
  const isCorrect = useSelector(sel.selectIsCorrect);
  const lessonType = useSelector(sel.selectLessonType);
  const repeatsHistory = useSelector(sel.selectLessonHistory);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    if (status === FinishPending) {
      history.push("lesson-result");
    }
    if (questions.length <= 0 && status.type < FinishPending.type) {
      history.push("");
    }
  }, [status, questions, history]);

  useEffect(() => {
    return () => {
      dispatch(actions.resetLesson());
    };
  }, [dispatch]);

  const correct = useCallback(() => {
    dispatch(actions.correct(isCorrect ? type.Correct : type.Accepted));
  }, [dispatch, isCorrect]);

  const wrong = useCallback(() => {
    dispatch(actions.wrong());
  }, [dispatch]);

  const check = useCallback(() => {
    dispatch(actions.check());
  }, [dispatch]);

  const showHistory = () => {
    setRepeatsPopup(true);
  };

  const hideHistory = () => {
    setRepeatsPopup(false);
  };

  const mainComponent = lessonType === 2 ? <Inserting /> : <Fiszka />;

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={status} />
      <button onClick={() => dispatch(actions.tickCard())}>Tick the card</button>
      <button onClick={() => showHistory()} disabled={repeatsHistory.length === 0}>
        Show history
      </button>
      {mainComponent}
      <RepeatsController
        onCheckClick={check}
        onCorrectClick={correct}
        onWrongClick={wrong}
        lessonState={status}
        isCorrect={isCorrect}
      />
      <RepeatHistory
        visible={repeatsPopup}
        onHide={hideHistory}
        history={[...repeatsHistory].reverse()}
      />
    </>
  );
}
