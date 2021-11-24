import "./Inserting.scss";
import { LessonState, LessonStateEnum } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import { ReactElement, useCallback, useEffect, useRef, useState } from "react";
import { useDispatch } from "react-redux";
import {
  check,
  correct,
  wrong,
  setAnswer as setAnswerAction,
} from "store/lesson/actions";
import Question from "../question/Question";

function Inserting({ state, repeat, isCorrect }: Model): ReactElement {
  const [answer, setAnswer] = useState("");
  const inputRef = useRef<any>(null);
  const modelRef = usePropRef({ state, repeat, isCorrect });
  const dispatch = useDispatch();

  const handleEventEvent = useCallback(
    (event: KeyboardEvent) => {
      if (event.key === "Enter") {
        switch (modelRef.current.state.type) {
          case LessonStateEnum.CheckPending: {
            dispatch(check());
            return;
          }
          case LessonStateEnum.AnswerPending: {
            dispatch(
              modelRef.current.isCorrect
                ? correct(
                    modelRef.current.repeat.groupId,
                    modelRef.current.repeat.cardId,
                    modelRef.current.repeat.questionSide,
                    1
                  )
                : wrong(
                    modelRef.current.repeat.groupId,
                    modelRef.current.repeat.cardId,
                    modelRef.current.repeat.questionSide
                  )
            );
            setAnswer("");
            if (inputRef.current) inputRef.current.focus();
            return;
          }
        }
      }
    },
    [dispatch, modelRef]
  );

  useEffect(() => {
    document.addEventListener("keydown", handleEventEvent);
    return () => {
      document.removeEventListener("keydown", handleEventEvent);
    };
  }, [handleEventEvent]);

  const onAnswerChanged = useCallback(
    (event: any) => {
      setAnswer(event.target.value);
      dispatch(setAnswerAction(event.target.value));
    },
    [dispatch]
  );
  console.log(modelRef.current.isCorrect);
  if (!state.card || !repeat) {
    return <></>;
  }
  return (
    <div>
      Question:
      <Question
        value={repeat.questionValue}
        example={repeat.questionExample}
        language={1}
      />
      <div>
        Answer:
        <input
          ref={inputRef}
          id="answer"
          name="answer"
          value={answer}
          onChange={onAnswerChanged}
          autoComplete="off"
          disabled={!state.inserting}
        />
        {state.answer && (
          <>
            <div className={isCorrect ? "correct" : "wrong"}>
              Poprawna odpowiedź: {repeat.answerValue}
            </div>
          </>
        )}
      </div>
    </div>
  );
}

interface Model {
  state: LessonState;
  repeat: Repeat;
  isCorrect: boolean | null;
}

export default Inserting;

function usePropRef<T = any>(props: T) {
  const propRef = useRef<T>(props);

  useEffect(() => {
    propRef.current = props;
  }, [props]);

  return propRef;
}
