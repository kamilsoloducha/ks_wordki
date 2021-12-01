import "./Inserting.scss";
import { LessonState } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import { ReactElement, useCallback, useEffect, useRef } from "react";
import Question from "../question/Question";
import Answer from "../answer/Answer";

export default function Inserting({
  state,
  repeat,
  isCorrect,
  insertedValue,
  onValueChanged,
  onEnterClick,
}: Model): ReactElement {
  const inputRef = useRef<any>(null);

  const handleEventEvent = useCallback(
    (event: KeyboardEvent) => {
      if (event.key !== "Enter") return;
      onEnterClick();
      inputRef.current?.focus();
    },
    [onEnterClick]
  );

  useEffect(() => {
    document.addEventListener("keydown", handleEventEvent);
    return () => {
      document.removeEventListener("keydown", handleEventEvent);
    };
  }, [handleEventEvent]);

  const onAnswerChanged = useCallback(
    (event: any) => {
      onValueChanged(event.target.value);
    },
    [onValueChanged]
  );

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
          value={insertedValue}
          onChange={onAnswerChanged}
          autoComplete="off"
          disabled={!state.inserting}
        />
        {state.answer && (
          <Answer
            userAnswer={insertedValue}
            correctAnswer={repeat.answerValue}
          />
        )}
      </div>
    </div>
  );
}

interface Model {
  state: LessonState;
  repeat: Repeat;
  isCorrect: boolean | null;
  insertedValue: string;
  onValueChanged: (value: string) => void;
  onEnterClick: () => void;
}

// function usePropRef<T = any>(props: T) {
//   const propRef = useRef<T>(props);

//   useEffect(() => {
//     propRef.current = props;
//   }, [props]);

//   return propRef;
// }
