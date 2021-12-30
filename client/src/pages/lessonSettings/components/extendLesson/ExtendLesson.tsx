import { ReactElement, useCallback, useEffect, useState } from "react";
import "./ExtendLesson.scss";
import * as lang from "common/models/languages";

export default function ExtendLesson({
  questionCount,
  selectionChanged,
}: Model): ReactElement {
  const [inputValue, setInputValue] = useState(0);
  const [lessonType, setLessonType] = useState(0);
  const [languageType, setLanguageType] = useState(0);

  useEffect(() => {
    selectionChanged({
      count: inputValue,
      lessonType,
      languageType,
    });
  }, [inputValue, lessonType, languageType]);

  const onInputChanged = useCallback((event: any) => {
    const newValue = event.target.value;
    setInputValue(newValue);
  }, []);

  const onAllClick = useCallback(() => {
    setInputValue(questionCount);
  }, [questionCount]);

  const onLessonTypeChanged = (lessonType: number) => {
    setLessonType(lessonType);
  };

  const onQuestionLanguageChanged = (value: number) => {
    setLanguageType(value);
  };

  return (
    <>
      <p>Available: {questionCount}</p>
      <form>
        Question Language:
        <br />
        <input
          type="radio"
          name="question"
          id="langPol"
          value={lang.Polish.type}
          onChange={() => onQuestionLanguageChanged(lang.Polish.type)}
        />
        <label htmlFor="langPol">Polish</label> <br />
        <input
          type="radio"
          name="question"
          id="langEng"
          value={lang.English.type}
          onChange={() => onQuestionLanguageChanged(lang.English.type)}
        />
        <label htmlFor="langEng">English</label>
      </form>
      <p>
        Selected:
        <input type="number" value={inputValue} onChange={onInputChanged} />
        <button onClick={onAllClick}>All</button>
      </p>
      <form>
        Lesson type:
        <br />
        <input
          type="radio"
          name="lessonType"
          id="1"
          value="1"
          onChange={() => onLessonTypeChanged(1)}
        />
        <label htmlFor="1">Fiszki</label> <br />
        <input
          type="radio"
          name="lessonType"
          id="2"
          value="2"
          onChange={() => onLessonTypeChanged(2)}
        />
        <label htmlFor="2">Typing</label>
      </form>
    </>
  );
}

interface Model {
  questionCount: number;
  selectionChanged: (value: LessonSettingsForm) => void;
}

export interface LessonSettingsForm {
  count: number;
  lessonType: number;
  languageType: number;
}
