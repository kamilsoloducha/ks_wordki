import * as key from "../../models/keyCodes";
import * as resultTypes from "../../models/resultTypes";
import * as actions from "store/lesson/actions";
import * as sel from "store/lesson/selectors";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Answer from "../answer/Answer";
import Question from "../question/Question";

export default function Fiszka(): ReactElement {
  const dispatch = useDispatch();
  const status = useSelector(sel.selectLessonState);
  const currectRepeat = useSelector(sel.selectCurrectRepeat);

  const handleEventEvent = useCallback(
    (event: KeyboardEvent) => {
      switch (event.key) {
        case key.Enter: {
          dispatch(actions.check());
          break;
        }
        case key.Left: {
          dispatch(actions.wrong());
          break;
        }
        case key.Right: {
          dispatch(actions.correct(resultTypes.Correct));
          break;
        }
      }
    },
    [dispatch]
  );

  useEffect(() => {
    document.addEventListener("keydown", handleEventEvent);
    return () => {
      document.removeEventListener("keydown", handleEventEvent);
    };
  }, [handleEventEvent]);

  if (!status.card || !currectRepeat) {
    return <></>;
  }

  return (
    <div>
      Question:
      <Question
        value={currectRepeat.question}
        example={currectRepeat.questionExample}
        language={1}
      />
      <Answer
        isVisible={status.answer}
        correctAnswer={currectRepeat.answer}
        exampleAnswer={currectRepeat.answerExample}
        userAnswer={currectRepeat.answer}
      />
    </div>
  );
}
