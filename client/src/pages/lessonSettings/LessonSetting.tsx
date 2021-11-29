import { ReactElement, useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { getCards, getCardsCount } from "store/lesson/actions";
import { selectCardsCount, selectRepeats } from "store/lesson/selectors";
import Settings from "./components/settings/Settings";

export default function LessonSettings(): ReactElement {
  const [selectedCount, setSelectedCount] = useState(0);
  const cardsCount = useSelector(selectCardsCount);
  const repeats = useSelector(selectRepeats);
  const dispatch = useDispatch();
  const history = useHistory();

  const selectionChanged = useCallback(
    (value: number) => {
      setSelectedCount(value);
    },
    [setSelectedCount]
  );

  const startLesson = () => {
    dispatch(getCards(selectedCount));
  };

  useEffect(() => {
    dispatch(getCardsCount());
  }, []); // eslint-disable-line

  useEffect(() => {
    if (repeats.length > 0) {
      history.push("lesson");
    }
  }, [repeats, history]);

  return (
    <>
      <Settings
        questionCount={cardsCount}
        selectionChanged={selectionChanged}
      />
      <button onClick={startLesson}>Start</button>
    </>
  );
}
