import "./LessonPage.scss";
import * as actions from "store/lesson/actions";
import * as sel from "store/lesson/selectors";
import * as type from "./models/resultTypes";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import Fiszka from "./components/fiszka/Fiszka";
import Inserting from "./components/inserting/Inserting";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { FinishPending } from "./models/lessonState";
import { LessonInformation } from "./components/lessonInformation/LessonInformation";

export default function LessonPage(): ReactElement {
  const questions = useSelector(sel.selectRepeats);
  const status = useSelector(sel.selectLessonState);
  const isCorrect = useSelector(sel.selectIsCorrect);
  const lessonType = useSelector(sel.selectLessonType);
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

  const mainComponent = lessonType === 2 ? <Inserting /> : <Fiszka />;

  return (
    <div className="lesson-page">
      {status.btnStart && (
        <button id="start-lesson-button" onClick={() => dispatch(actions.startLesson())}>
          Start
        </button>
      )}
      <LessonInformation />
      {mainComponent}
      <RepeatsController
        onCheckClick={check}
        onCorrectClick={correct}
        onWrongClick={wrong}
        lessonState={status}
        isCorrect={isCorrect}
      />
    </div>
  );
}
