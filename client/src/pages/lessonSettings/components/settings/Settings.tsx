import { ReactElement, useCallback, useState } from "react";

export default function Settigs({
  questionCount,
  selectionChanged,
}: Model): ReactElement {
  const [inputValue, setInputValue] = useState(0);

  const onInputChanged = useCallback(
    (event: any) => {
      const newValue = event.target.value;
      setInputValue(newValue);
      selectionChanged(newValue);
    },
    [selectionChanged]
  );

  const onAllClick = useCallback(() => {
    setInputValue(questionCount);
    selectionChanged(questionCount);
  }, [questionCount, selectionChanged]);

  return (
    <>
      <p>Available: {questionCount}</p>
      <p>
        Selected:
        <input type="number" value={inputValue} onChange={onInputChanged} />
        <button onClick={onAllClick}>All</button>
      </p>
    </>
  );
}

interface Model {
  questionCount: number;
  selectionChanged: (value: number) => void;
}
