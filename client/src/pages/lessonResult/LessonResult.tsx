import "./LessonResult.scss";
import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { getCards, resetLesson } from "store/lesson/actions";
import { selectLessonHistory, selectResults, selectSettings } from "store/lesson/selectors";
import { History } from "pages/lesson/components/history/History";
import { LessonMode } from "pages/lessonSettings/models/lesson-mode";
import UserRepeat from "pages/lesson/models/userRepeat";

export default function LessonResult(): ReactElement {
  const dispatch = useDispatch();
  const history = useHistory();
  const results = useSelector(selectResults);
  const lessonHistory = useSelector(selectLessonHistory);
  const lessonSettings = useSelector(selectSettings);

  const [userRepeats, setUserRepeats] = useState<UserRepeat[]>(lessonHistory);

  const userAnswerColumn = userAnswerColumnNecessary(lessonSettings.type);

  useEffect(() => {
    dispatch(resetLesson());
  }, [dispatch]);

  const onContinue = useCallback(() => {
    dispatch(getCards());
  }, [history, dispatch]);

  const startNew = useCallback(() => {
    history.push("/lesson-settings");
  }, [history]);

  const finish = useCallback(() => {
    history.push("/dashboard");
  }, [history]);

  const showCorrect = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, 1));
  };

  const showAccepted = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, 0));
  };

  const showWrong = () => {
    setUserRepeats(filterLessonHistory(lessonHistory, -1));
  };

  const showAll = () => {
    setUserRepeats(lessonHistory);
  };

  return (
    <div className="lesson-results-container">
      <div className="buttons">
        <button onClick={onContinue}>Continue</button>
        <button onClick={startNew}>Start New</button>
        <button onClick={finish}>Finish</button>
      </div>
      <div className="details">
        <div className="overview">
          <b>Results:</b>
          <p onClick={showCorrect}>Correct: {results.correct}</p>
          <p onClick={showAccepted}>Accepeted: {results.accept}</p>
          <p onClick={showWrong}>Wrong: {results.wrong}</p>
          <p onClick={showAll}>Answers: {results.answers}</p>
        </div>
        <div className="table">
          <History history={userRepeats} userAnswer={userAnswerColumn} />
        </div>
      </div>
    </div>
  );
}

export function userAnswerColumnNecessary(type: number): boolean {
  return type === 2;
}

export function filterLessonHistory(items: UserRepeat[], result: number): UserRepeat[] {
  return items.filter((x) => x.result === result);
}
