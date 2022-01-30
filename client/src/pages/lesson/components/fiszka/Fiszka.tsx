import { LessonState } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import { ReactElement } from "react";
import Answer from "../answer/Answer";
import Question from "../question/Question";

export default function Fiszka({
  lessonState: state,
  repeat,
}: Model): ReactElement {
  if (!state.card || !repeat) {
    return <></>;
  }
  return (
    <div>
      Question:
      <Question
        value={repeat.question}
        example={repeat.questionExample}
        language={1}
      />
      <Answer
        isVisible={state.answer}
        correctAnswer={repeat.answer}
        exampleAnswer={repeat.answerExample}
        userAnswer={repeat.answer}
      />
    </div>
  );
}

interface Model {
  lessonState: LessonState;
  repeat: Repeat;
}
