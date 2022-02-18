import "./NewCardsSettings.scss";
import * as act from "store/lesson/actions";
import { ReactElement, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { selectSettings } from "store/lesson/selectors";
import { Group } from "pages/lessonSettings/models/group";
import { LessonTypeSelector } from "../lessonTypeSelector/LessonTypeSelector";
import { LanguageSelector } from "../languageSelector/LanguageSelector";
import { CountSelector } from "../countSelector/CountSelector";
import { LessonSettings } from "pages/lessonSettings/models/lessonSettings";
import { GroupSelector } from "../groupSelector/GroupSelector";

export default function NewCardsSettings(): ReactElement {
  const dispatch = useDispatch();
  const settings = useSelector(selectSettings);
  const groups = filterGroups(settings.groups, settings.languages);

  useEffect(() => {
    dispatch(act.getGroups());
  }, [dispatch]);

  return (
    <>
      <div className="setting-item">
        <LanguageSelector
          selected={settings.languages}
          onSelectedChanged={(value) => dispatch(act.setSettingLanguage(value))}
        />
      </div>
      <div className="setting-item">
        <GroupSelector
          items={groups}
          selectedGroup={settings.selectedGroup}
          onSelectedChanged={(value) => dispatch(act.setSettingGroup(value))}
        />
      </div>
      <div className="setting-item">
        <CountSelector
          all={getAllCount(settings)}
          selected={settings.count}
          onSelectedChanged={(value) => dispatch(act.setSettingCount(value))}
        />
      </div>
      <div className="setting-item">
        <LessonTypeSelector
          selected={settings.type}
          onSelectedChanged={(value) => dispatch(act.setSettingType(value))}
        />
      </div>
    </>
  );
}

function filterGroups(groups: Group[], languages: number[]): Group[] {
  if (languages.includes(1)) {
    return groups.filter((x) => x.backCount > 0);
  }
  if (languages.includes(2)) {
    return groups.filter((x) => x.frontCount > 0);
  }
  return groups;
}

function getAllCount(settings: LessonSettings): number {
  if (!settings.selectedGroup) {
    return 0;
  }
  let count = 0;
  if (settings.languages.length === 0) {
    count += settings.selectedGroup.backCount;
    count += settings.selectedGroup.frontCount;
  }
  if (settings.languages.includes(1)) {
    const increase =
      settings.selectedGroup.front === 1
        ? settings.selectedGroup.frontCount
        : settings.selectedGroup.backCount;
    count += increase;
  }
  if (settings.languages.includes(2)) {
    const increase =
      settings.selectedGroup.front === 2
        ? settings.selectedGroup.frontCount
        : settings.selectedGroup.backCount;
    count += increase;
  }
  return count;
}
