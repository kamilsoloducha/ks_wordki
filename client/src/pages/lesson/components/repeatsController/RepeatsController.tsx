import "./RepeatsController.scss";
import { LessonState } from "pages/lesson/models/lessonState";
import { ReactElement } from "react";

export default function RepeatsController({
  lessonState,
  isCorrect,
  onCheckClick,
  onCorrectClick,
  onWrongClick,
}: Model): ReactElement {
  return (
    <div className="repeats-controller-container">
      {lessonState.btnCheck && (
        <button className={"check focused"} onClick={onCheckClick}>
          Check
        </button>
      )}
      {lessonState.btnCorrect && (
        <button
          className={`correct ${isCorrect ? "focused" : ""}`}
          onClick={onCorrectClick}
        >
          Correct
        </button>
      )}
      {lessonState.btnWrong && (
        <button
          className={`wrong ${isCorrect ? "" : "focused"}`}
          onClick={onWrongClick}
        >
          Wrong
        </button>
      )}
    </div>
  );
}

interface Model {
  isCorrect: boolean | null;
  lessonState: LessonState;
  onCheckClick: () => void;
  onCorrectClick: () => void;
  onWrongClick: () => void;
}
