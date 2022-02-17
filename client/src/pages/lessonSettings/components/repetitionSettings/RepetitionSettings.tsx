import { ReactElement, useEffect } from "react";
import { LessonTypeSelector } from "../lessonTypeSelector/LessonTypeSelector";
import { LanguageSelector } from "../languageSelector/LanguageSelector";
import { useDispatch, useSelector } from "react-redux";
import { setSettingCount, setSettingLanguage, setSettingType } from "store/lesson/actions";
import { CountSelector } from "../countSelector/CountSelector";
import { selectCardsCount, selectSettings } from "store/lesson/selectors";

export default function Settings(): ReactElement {
  const cardsCount = useSelector(selectCardsCount);
  const settings = useSelector(selectSettings);
  const dispatch = useDispatch();

  return (
    <>
      <div className="setting-item">
        <LanguageSelector
          selected={settings.languages}
          onSelectedChanged={(value) => dispatch(setSettingLanguage(value))}
        />
      </div>
      <div className="setting-item">
        <CountSelector
          all={cardsCount ?? 0}
          selected={settings.count}
          onSelectedChanged={(value) => dispatch(setSettingCount(value))}
        />
      </div>
      <div className="setting-item">
        <LessonTypeSelector
          selected={settings.type}
          onSelectedChanged={(value) => dispatch(setSettingType(value))}
        />
      </div>
    </>
  );
}
