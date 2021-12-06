import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import * as actions from "store/lesson/actions";
import {
  selectCurrectRepeat,
  selectIsCorrect,
  selectLessonState,
  selectRepeats,
} from "store/lesson/selectors";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { LessonState, LessonStateEnum } from "./models/lessonState";

export default function LessonPage(): ReactElement {
  const [insertedValue, setInsertedValue] = useState("");
  const questions = useSelector(selectRepeats);
  const lessonState = useSelector(selectLessonState);
  const currectRepeat = useSelector(selectCurrectRepeat);
  const isCorrect = useSelector(selectIsCorrect);
  const state = LessonState.getState(lessonState);
  const dispatch = useDispatch();
  const history = useHistory();

  useEffect(() => {
    if (lessonState === LessonStateEnum.FinishPending) {
      history.push("lesson-result");
    }
    if (questions.length <= 0 && state.type < LessonStateEnum.FinishPending) {
      history.push("");
    }
  }, [lessonState, questions, history, state.type]);

  useEffect(() => {
    return () => {
      dispatch(actions.resetLesson());
    };
  }, [dispatch]);

  const onValueChanged = useCallback(
    (value: string) => {
      setInsertedValue(value);
      dispatch(actions.setAnswer(value));
    },
    [setInsertedValue, dispatch]
  );

  const correct = useCallback(() => {
    dispatch(
      actions.correct(
        currectRepeat.groupId,
        currectRepeat.cardId,
        currectRepeat.questionSide,
        1
      )
    );
    setInsertedValue("");
  }, [dispatch, currectRepeat]);

  const wrong = useCallback(() => {
    dispatch(
      actions.wrong(
        currectRepeat.groupId,
        currectRepeat.cardId,
        currectRepeat.questionSide
      )
    );
    setInsertedValue("");
  }, [dispatch, currectRepeat]);

  const check = useCallback(() => {
    dispatch(actions.check());
  }, [dispatch]);

  const onEnterClick = useCallback(() => {
    switch (state.type) {
      case LessonStateEnum.CheckPending: {
        check();
        return;
      }
      case LessonStateEnum.AnswerPending: {
        isCorrect ? correct() : wrong();
        return;
      }
    }
  }, [state, check, correct, wrong, isCorrect]);

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={state} />
      <Inserting
        insertedValue={insertedValue}
        onValueChanged={onValueChanged}
        onEnterClick={onEnterClick}
        state={state}
        isCorrect={isCorrect}
        repeat={currectRepeat}
      />
      <RepeatsController
        onCheckClick={check}
        onCorrectClick={correct}
        onWrongClick={wrong}
        lessonState={state}
        isCorrect={isCorrect}
      />
    </>
  );
}
