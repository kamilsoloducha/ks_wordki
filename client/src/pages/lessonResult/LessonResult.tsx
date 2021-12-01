import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { resetLesson } from "store/lesson/actions";
import { selectResults } from "store/lesson/selectors";

export default function LessonResult(): ReactElement {
  const dispatch = useDispatch();
  const results = useSelector(selectResults);

  useEffect(() => {
    dispatch(resetLesson());
  }, [dispatch]);

  return (
    <div>
      <p>Results:</p>
      <p>Correct: {results.correct}</p>
      <p>Accepeted: {results.accept}</p>
      <p>Wrong: {results.wrong}</p>
      <p>Answers: {results.answers}</p>
    </div>
  );
}
