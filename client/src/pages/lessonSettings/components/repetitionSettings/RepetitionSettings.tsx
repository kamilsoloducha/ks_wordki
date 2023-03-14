import { ReactElement } from "react";
import { LessonTypeSelector } from "../lessonTypeSelector/LessonTypeSelector";
import { LanguageSelector } from "../languageSelector/LanguageSelector";
import { useDispatch, useSelector } from "react-redux";
import { CountSelector } from "../countSelector/CountSelector";
import { selectCardsCount, selectLanguages, selectSettings } from "store/lesson/selectors";
import { setSettingsCount, setSettingsLanguage, setSettingsType } from "store/lesson/reducer";

export default function Settings(): ReactElement {
  const languages = useSelector(selectLanguages);
  const cardsCount = useSelector(selectCardsCount);
  const settings = useSelector(selectSettings);
  const dispatch = useDispatch();

  return (
    <>
      <div className="setting-item">
        <LanguageSelector
          languages={languages}
          selected={settings.languages}
          onSelectedChanged={(value) => dispatch(setSettingsLanguage({ languages: value }))}
        />
      </div>
      <div className="setting-item">
        <CountSelector
          all={cardsCount ?? 0}
          selected={settings.count}
          onSelectedChanged={(value) => dispatch(setSettingsCount({ count: value }))}
        />
      </div>
      <div className="setting-item">
        <LessonTypeSelector
          selected={settings.type}
          onSelectedChanged={(value) => dispatch(setSettingsType({ type: value }))}
        />
      </div>
    </>
  );
}
