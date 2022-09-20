import "./LessonResult.scss";
import * as actions from "store/lesson/reducer";
import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { selectLessonHistory, selectResults, selectSettings } from "store/lesson/selectors";
import { History } from "pages/lesson/components/history/History";
import UserRepeat from "pages/lesson/models/userRepeat";
import CardDialog from "common/components/dialogs/cardDialog/CardDialog";
import { FormModel } from "common/components/dialogs/cardDialog/CardForm";
import { Side } from "common/models/side";
import { useTitle } from "common";

export default function LessonResult(): ReactElement {
  useTitle("Wordki - Results");
  const dispatch = useDispatch();
  const history = useNavigate();
  const results = useSelector(selectResults);
  const lessonHistory = useSelector(selectLessonHistory);
  const lessonSettings = useSelector(selectSettings);

  const [userRepeats, setUserRepeats] = useState<UserRepeat[]>(lessonHistory);
  const [selectedItem, setSelectedItem] = useState<UserRepeat | null>(null);

  const userAnswerColumn = userAnswerColumnNecessary(lessonSettings.type);

  useEffect(() => {
    dispatch(actions.resetLesson());
  }, [dispatch]);

  const onContinue = useCallback(() => {
    dispatch(actions.getCards());
  }, [dispatch]);

  const startNew = useCallback(() => {
    history("/lesson-settings");
  }, [history]);

  const finish = useCallback(() => {
    history("/dashboard");
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

  const onSubmit = (form: FormModel) => {
    if (!selectedItem) return;
    form.backEnabled = null;
    form.frontEnabled = null;
    dispatch(actions.updateCard({ form, groupId: selectedItem.repeat.groupId }));
    setSelectedItem(null);
  };

  const onDelete = (form: FormModel) => {
  };

  return (
    <>
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
            <History
              history={userRepeats}
              userAnswer={userAnswerColumn}
              onItemClick={(item) => setSelectedItem(item)}
            />
          </div>
        </div>
      </div>
      <CardDialog
        card={getFormModelFromUserRepeat(selectedItem)}
        onHide={() => setSelectedItem(null)}
        onSubmit={onSubmit}
        onDelete={onDelete}
      />
    </>
  );
}

export function userAnswerColumnNecessary(type: number): boolean {
  return type === 2;
}

export function filterLessonHistory(items: UserRepeat[], result: number): UserRepeat[] {
  return items.filter((x) => x.result === result);
}

export function getFormModelFromUserRepeat(userRepeat: UserRepeat | null): FormModel | null {
  if (!userRepeat) return null;

  const form: FormModel = {
    cardId: userRepeat.repeat.cardId,
    frontValue:
      userRepeat.repeat.questionSide === Side.Front
        ? userRepeat.repeat.question
        : userRepeat.repeat.answer,
    frontExample:
      userRepeat.repeat.questionSide === Side.Front
        ? userRepeat.repeat.questionExample
        : userRepeat.repeat.answerExample,
    frontEnabled: true,
    backValue:
      userRepeat.repeat.questionSide === Side.Front
        ? userRepeat.repeat.answer
        : userRepeat.repeat.question,
    backExample:
      userRepeat.repeat.questionSide === Side.Front
        ? userRepeat.repeat.answerExample
        : userRepeat.repeat.questionExample,
    backEnabled: false,
    comment: "",
    isTicked: false,
  };

  return form;
}
