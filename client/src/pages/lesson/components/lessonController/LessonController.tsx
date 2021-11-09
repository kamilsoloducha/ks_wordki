import { LessonState } from "pages/lesson/models/lessonState";
import { ReactElement } from "react";
import { useDispatch } from "react-redux";
import { finishLesson, pauseLesson, startLesson } from "store/lesson/actions";

function LessonController({ lessonState }: Model): ReactElement {
  const dispatch = useDispatch();

  const onStart = () => {
    dispatch(startLesson());
  };

  const onPause = () => {
    dispatch(pauseLesson());
  };

  const onFinish = () => {
    dispatch(finishLesson());
  };

  return (
    <>
      {lessonState.btnStart && <button onClick={onStart}>Start</button>}
      {lessonState.btnPause && <button onClick={onPause}>Przerwa</button>}
      {lessonState.btnFinish && <button onClick={onFinish}>Koniec</button>}
    </>
  );
}

export default LessonController;

interface Model {
  lessonState: LessonState;
}
