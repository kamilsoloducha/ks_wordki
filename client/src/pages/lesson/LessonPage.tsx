import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import * as actions from "store/lesson/actions";
import * as sel from "store/lesson/selectors";
import Fiszka from "./components/fiszka/Fiszka";
import Inserting from "./components/inserting/Inserting";
import LessonController from "./components/lessonController/LessonController";
import RepeatsController from "./components/repeatsController/RepeatsController";
import { LessonState, LessonStateEnum } from "./models/lessonState";
import { Repeat } from "./models/repeat";

export default function LessonPage(): ReactElement {
  const [insertedValue, setInsertedValue] = useState("");
  const questions = useSelector(sel.selectRepeats);
  const lessonState = useSelector(sel.selectLessonState);
  const currectRepeat = useSelector(sel.selectCurrectRepeat);
  const isCorrect = useSelector(sel.selectIsCorrect);
  const lessonType = useSelector(sel.selectLessonType);
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
    dispatch(actions.correct(currectRepeat.sideId, 1));
    setInsertedValue("");
  }, [dispatch, currectRepeat]);

  const wrong = useCallback(() => {
    dispatch(actions.wrong(currectRepeat.sideId));
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

  const tickCard = useCallback(
    (repeat: Repeat) => {
      dispatch(actions.tickCard(repeat.sideId));
    },
    [dispatch]
  );

  const mainComponent =
    lessonType === 2 ? (
      <Inserting
        insertedValue={insertedValue}
        onValueChanged={onValueChanged}
        onEnterClick={onEnterClick}
        state={state}
        isCorrect={isCorrect}
        repeat={currectRepeat}
      />
    ) : (
      <Fiszka lessonState={state} repeat={currectRepeat} />
    );

  return (
    <>
      <div>Pozostało: {questions.length}</div>
      <LessonController lessonState={state} />
      <button
        onClick={() => {
          tickCard(currectRepeat);
        }}
      >
        Tick the card
      </button>
      {mainComponent}
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
