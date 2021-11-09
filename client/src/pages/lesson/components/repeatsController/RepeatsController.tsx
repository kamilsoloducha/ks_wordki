import { LessonState } from "pages/lesson/models/lessonState";
import { ReactElement } from "react";
import { useDispatch, useSelector } from "react-redux";
import { check, correct, wrong } from "store/lesson/actions";
import { selectCurrectRepeat } from "store/lesson/selectors";

function RepeatsController({ lessonState }: Model): ReactElement {
  const dispatch = useDispatch();
  const currectRepeat = useSelector(selectCurrectRepeat);

  const onCheck = () => {
    dispatch(check());
  };

  const onCorrect = () => {
    dispatch(
      correct(
        currectRepeat.groupId,
        currectRepeat.cardId,
        currectRepeat.questionSide,
        1
      )
    );
  };

  const onWrong = () => {
    dispatch(
      wrong(
        currectRepeat.groupId,
        currectRepeat.cardId,
        currectRepeat.questionSide
      )
    );
  };
  return (
    <>
      {lessonState.btnCheck && <button onClick={onCheck}>Sprawdź</button>}
      {lessonState.btnCorrect && <button onClick={onCorrect}>Dobrze</button>}
      {lessonState.btnWrong && <button onClick={onWrong}>Źle</button>}
    </>
  );
}

export default RepeatsController;

interface Model {
  lessonState: LessonState;
}
