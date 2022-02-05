import { ReactElement, useCallback, useEffect, useState } from "react";
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
  const [lessonType, setLessonType] = useState(settings.type);
  const [language, setLanguage] = useState(settings.language);

  useEffect(() => {
    if (inputValue > questionCount) {
      setInputValue(questionCount);
    }
  }, [questionCount, setInputValue, inputValue]);

  const onAllClick = useCallback(() => {
    setInputValue(questionCount);
    countChanged(questionCount);
  }, [questionCount, setInputValue, countChanged]);

  const onInputChanged = useCallback(
    (event: any) => {
      let value = event.target.value as number;
      if (value < 0) {
        value = 0;
      }
      if (value > questionCount) {
        value = questionCount;
      }
      setInputValue(value);
      countChanged(event.target.value);
    },
    [setInputValue, countChanged, questionCount]
  );

  const onLessonTypeChanged = (lessonType: number) => {
    setLessonType(lessonType);
    typeChanged(lessonType);
  };

  const onQuestionLanguageChanged = (value: number) => {
    setLanguage(value);
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
          checked={language === 1}
        />
        <label htmlFor="langPol">Polish</label> <br />
        <input
          type="radio"
          name="question"
          id="langEng"
          value={lang.English.type}
          onChange={() => onQuestionLanguageChanged(lang.English.type)}
          checked={language === 2}
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
          checked={lessonType === 1}
        />
        <label htmlFor="1">Fiszki</label> <br />
        <input
          type="radio"
          name="lessonType"
          id="2"
          value="2"
          onChange={() => onLessonTypeChanged(2)}
          checked={lessonType === 2}
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
