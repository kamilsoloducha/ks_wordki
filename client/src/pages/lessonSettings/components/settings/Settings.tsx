import { ReactElement, useCallback, useState } from "react";
import * as lang from "common/models/languages";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";

export default function Settings({
  settings,
  questionCount,
  languageChanged,
  countChanged,
  typeChanged,
}: Model): ReactElement {
  const [inputValue, setInputValue] = useState(settings.count);
  const [, setLessonType] = useState(settings.type);
  const [, setLanguageType] = useState(settings.language);

  const onAllClick = useCallback(() => {
    setInputValue(questionCount);
    countChanged(questionCount);
  }, [questionCount, setInputValue, countChanged]);

  const onInputChanged = useCallback(
    (event: any) => {
      setInputValue(event.target.value);
      countChanged(event.target.value);
    },
    [setInputValue, countChanged]
  );

  const onLessonTypeChanged = (lessonType: number) => {
    setLessonType(lessonType);
    typeChanged(lessonType);
  };

  const onQuestionLanguageChanged = (value: number) => {
    setLanguageType(value);
    languageChanged(value);
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
  settings: LessonSettings;
  questionCount: number;
  languageChanged: (value: number) => void;
  countChanged: (value: number) => void;
  typeChanged: (value: number) => void;
}
