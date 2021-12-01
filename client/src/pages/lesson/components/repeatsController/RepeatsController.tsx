import { LessonState } from "pages/lesson/models/lessonState";
import { ReactElement } from "react";

export default function RepeatsController({
  lessonState,
  onCheckClick,
  onCorrectClick,
  onWrongClick,
}: Model): ReactElement {
  return (
    <>
      {lessonState.btnCheck && <button onClick={onCheckClick}>Sprawdź</button>}
      {lessonState.btnCorrect && (
        <button onClick={onCorrectClick}>Dobrze</button>
      )}
      {lessonState.btnWrong && <button onClick={onWrongClick}>Źle</button>}
    </>
  );
}

interface Model {
  lessonState: LessonState;
  onCheckClick: () => void;
  onCorrectClick: () => void;
  onWrongClick: () => void;
}
