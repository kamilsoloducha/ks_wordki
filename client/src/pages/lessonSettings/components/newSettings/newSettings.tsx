import "./NewSettings.scss";
import * as act from "store/lesson/actions";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { selectSettings } from "store/lesson/selectors";
import { Dropdown } from "primereact/dropdown";
import { Group } from "pages/lessonSettings/models/group";
import { LessonTypeSelector } from "../lessonTypeSelector/LessonTypeSelector";
import { LanguageSelector } from "../languageSelector/LanguageSelector";

export default function NewSettings(): ReactElement {
  const dispatch = useDispatch();
  const settings = useSelector(selectSettings);
  const groups = filterGroups(settings.groups, settings.languages);

  useEffect(() => {
    dispatch(act.getGroups());
  }, [dispatch]);

  const onSelectedGroupChanged = useCallback(
    (event$: any) => {
      dispatch(act.setSettingGroup(event$.value));
    },
    [dispatch]
  );

  const onLimitChanged = useCallback(
    (event$: any) => {
      let limit = parseInt(event$.target.value);
      if (limit < 0) {
        limit = 0;
      }
      dispatch(act.setSettingCount(limit));
    },
    [dispatch]
  );

  const dropdownItemLayout = (option: Group, props: any) => {
    if (option) {
      return (
        <>
          {option.name} - front: {option.frontCount} - back: {option.backCount}
        </>
      );
    }
    return (
      <>
        <span>{props.placeholder}</span>
      </>
    );
  };

  return (
    <>
      <form className="settings-form">
        <LanguageSelector
          selected={settings.languages}
          onSelectedChanged={(value) => dispatch(act.setSettingLanguage(value))}
        />
        <Dropdown
          value={settings.selectedGroup}
          options={groups}
          onChange={onSelectedGroupChanged}
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          optionLabel="name"
          placeholder="Select group..."
        />
        <br />
        <br />
        Count:
        <input type="number" value={settings.count} onChange={onLimitChanged} />
        <LessonTypeSelector
          selected={settings.type}
          onSelectedChanged={(value) => dispatch(act.setSettingType(value))}
        />
      </form>
      <br />
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
