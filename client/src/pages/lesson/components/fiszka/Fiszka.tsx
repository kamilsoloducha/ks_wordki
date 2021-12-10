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
          <div>
            {repeat.answerValue} : {repeat.answerExample}
          </div>
        </>
      )}
    </div>
  );
}

interface Model {
  lessonState: LessonState;
  repeat: Repeat;
}
