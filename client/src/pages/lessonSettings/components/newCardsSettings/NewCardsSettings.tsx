import "./NewCardsSettings.scss";
import * as act from "store/lesson/reducer";
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
          onSelectedChanged={(value) => dispatch(act.setSettingsLanguage({ languages: value }))}
        />
      </div>
      <div className="setting-item">
        <GroupSelector
          items={groups}
          selectedGroupId={settings.selectedGroupId}
          onSelectedChanged={(value) => dispatch(act.setSettingsGroup({ groupId: value }))}
        />
      </div>
      <div className="setting-item">
        <CountSelector
          all={getAllCount(settings)}
          selected={settings.count}
          onSelectedChanged={(value) => dispatch(act.setSettingsCount({ count: value }))}
        />
      </div>
      <div className="setting-item">
        <LessonTypeSelector
          selected={settings.type}
          onSelectedChanged={(value) => dispatch(act.setSettingsType({ type: value }))}
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
  if (!settings.selectedGroupId) {
    return 0;
  }
  const selectedGroup = settings.groups.find((x) => x.id === settings.selectedGroupId);
  if (!selectedGroup) {
    return 0;
  }
  let count = 0;
  if (settings.languages.length === 0) {
    count += selectedGroup.backCount;
    count += selectedGroup.frontCount;
  }
  if (settings.languages.includes(1)) {
    const increase = selectedGroup.front === 1 ? selectedGroup.frontCount : selectedGroup.backCount;
    count += increase;
  }
  if (settings.languages.includes(2)) {
    const increase = selectedGroup.front === 2 ? selectedGroup.frontCount : selectedGroup.backCount;
    count += increase;
  }
  return count;
}
