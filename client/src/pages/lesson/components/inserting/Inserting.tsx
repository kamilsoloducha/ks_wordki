import * as select from "store/lesson/selectors";
import * as act from "store/lesson/actions";
import * as rt from "../../models/resultTypes";
import "./Inserting.scss";
import { CheckPending, LessonStateEnum } from "pages/lesson/models/lessonState";
import { ReactElement, useCallback, useEffect, useRef } from "react";
import Question from "../question/Question";
import Answer from "../answer/Answer";
import { useDispatch, useSelector } from "react-redux";

export default function Inserting(): ReactElement {
  const dispatch = useDispatch();
  const inputRef = useRef<any>(null);
  const answer = useSelector(select.selectUserAnswer);
  const repeat = useSelector(select.selectCurrectRepeat);
  const status = useSelector(select.selectLessonState);
  const isCorrect = useSelector(select.selectIsCorrect);

  const handleEventEvent = useCallback(
    (event: KeyboardEvent) => {
      if (event.key !== "Enter") return;
      switch (status.type) {
        case LessonStateEnum.CheckPending: {
          dispatch(act.check());
          return;
        }
        case LessonStateEnum.AnswerPending: {
          dispatch(isCorrect ? act.correct(rt.Correct) : act.wrong());
          return;
        }
      }
      inputRef.current?.focus();
    },
    [dispatch, isCorrect, status]
  );

  useEffect(() => {
    document.addEventListener("keydown", handleEventEvent);
    return () => {
      document.removeEventListener("keydown", handleEventEvent);
    };
  }, [handleEventEvent]);

  useEffect(() => {
    if (status === CheckPending) {
      dispatch(act.setAnswer(""));
      inputRef.current?.focus();
    }
  }, [status, inputRef, dispatch]);

  const onAnswerChanged = useCallback(
    (event: any) => {
      const answer = event.target.value;
      dispatch(act.setAnswer(answer));
    },
    [dispatch]
  );

  if (!status.card || !repeat) {
    return <></>;
  }

  return (
    <>
      <Question value={repeat.question} example={repeat.questionExample} language={1} />
      <input
        ref={inputRef}
        id="answer"
        value={answer}
        onChange={onAnswerChanged}
        autoComplete="off"
        disabled={!status.inserting}
      />
      <Answer
        isVisible={status.answer}
        userAnswer={answer}
        correctAnswer={repeat.answer}
        exampleAnswer={repeat.answerExample}
      />
    </>
  );
}
