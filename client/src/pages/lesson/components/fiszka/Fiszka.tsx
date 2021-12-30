import { LessonState } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import { ReactElement } from "react";
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
        value={repeat.questionValue}
        example={repeat.questionExample}
        language={1}
      />
      {state.answer && (
        <>
          Answer:
          <Question
            value={repeat.answerValue}
            example={repeat.answerExample}
            language={1}
          />
        </>
      )}
    </div>
  );
}

interface Model {
  lessonState: LessonState;
  repeat: Repeat;
}
