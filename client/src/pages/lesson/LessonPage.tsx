import "./LessonPage.scss";
import * as actions from "store/lesson/reducer";
import * as sel from "store/lesson/selectors";
import * as type from "./models/resultTypes";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import Fiszka from "./components/fiszka/Fiszka";
import Inserting from "./components/inserting/Inserting";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { FinishPending } from "./models/lessonState";
import { LessonInformation } from "./components/lessonInformation/LessonInformation";
import { useTitle } from "common";
import { useEffectOnce } from "common/hooks/useEffectOnce";

export default function LessonPage(): ReactElement {
  useTitle("Wordki - Lesson");
  const questions = useSelector(sel.selectRepeats);
  const status = useSelector(sel.selectLessonState);
  const isCorrect = useSelector(sel.selectIsCorrect);
  const lessonType = useSelector(sel.selectLessonType);
  const dispatch = useDispatch();
  const history = useNavigate();

  useEffect(() => {
    if (status === FinishPending) {
      history("/lesson-result");
    }
    if (questions.length <= 0 && status.type < FinishPending.type) {
      history("/dashboard");
    }
  }, [status, questions, history, dispatch]);

  useEffectOnce(() => {
    dispatch(actions.resetResults());
    return () => {
      dispatch(actions.resetLesson());
    };
  });

  const correct = useCallback(() => {
    dispatch(actions.correct({ result: isCorrect ? type.Correct : type.Accepted }));
  }, [dispatch, isCorrect]);

  const wrong = useCallback(() => {
    dispatch(actions.wrong({ result: -1 }));
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
