import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { resetLesson } from "store/lesson/actions";
import { selectResults } from "store/lesson/selectors";

export default function LessonResult(): ReactElement {
  const dispatch = useDispatch();
  const history = useHistory();
  const results = useSelector(selectResults);

  useEffect(() => {
    dispatch(resetLesson());
  }, [dispatch]);

  const onContinue = useCallback(() => {
    history.push("/lesson-settings");
  }, [history]);

  return (
    <div>
      <p>Results:</p>
      <p>Correct: {results.correct}</p>
      <p>Accepeted: {results.accept}</p>
      <p>Wrong: {results.wrong}</p>
      <p>Answers: {results.answers}</p>
      <button onClick={onContinue}>Continue</button>
    </div>
  );
}
