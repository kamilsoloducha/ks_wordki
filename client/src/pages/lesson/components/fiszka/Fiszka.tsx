import { LessonState } from "pages/lesson/models/lessonState";
import { Repeat } from "pages/lesson/models/repeat";
import { ReactElement } from "react";

function Fiszka({ lessonState: state, repeat }: Model): ReactElement {
  if (!state.card || !repeat) {
    return <></>;
  }
  return (
    <div>
      Question:
      <div>
        {repeat.questionValue} : {repeat.questionExample}
      </div>
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

export default Fiszka;

interface Model {
  lessonState: LessonState;
  repeat: Repeat;
}
