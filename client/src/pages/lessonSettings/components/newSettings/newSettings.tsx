import * as act from "store/lesson/actions";
import * as lang from "common/models/languages";
import { ReactElement, useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { selectSettings } from "store/lesson/selectors";
import { Dropdown } from "primereact/dropdown";
import { Group } from "pages/lessonSettings/models/group";

export default function NewSettings(): ReactElement {
  const dispatch = useDispatch();
  const settings = useSelector(selectSettings);
  const groups = filterGroups(settings.groups, settings.language);

  useEffect(() => {
    dispatch(act.getGroups());
  }, [dispatch]);

  const onLanguageChanged = useCallback(
    (event$: any) => {
      const language = parseInt(event$.target.value);
      dispatch(act.setSettingLanguage(language));
    },
    [dispatch]
  );

  const onSelectedGroupChanged = useCallback(
    (event$: any) => {
      dispatch(act.setSettingGroup(event$.value));
    },
    [dispatch]
  );

  const onTypeChanged = useCallback(
    (event$: any) => {
      const type = parseInt(event$.target.value);
      dispatch(act.setSettingType(type));
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
      <form>
        <br />
        Question Language:
        <input
          type="radio"
          name="question"
          id="langPol"
          value={lang.Polish.type}
          onChange={onLanguageChanged}
          checked={settings.language === 1}
        />
        <label htmlFor="langPol">Polish</label>
        <input
          type="radio"
          name="question"
          id="langEng"
          value={lang.English.type}
          onChange={onLanguageChanged}
          checked={settings.language === 2}
        />
        <label htmlFor="langEng">English</label>
        <br />
        <br />
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
        <br />
        <br />
        Lesson type:
        <input
          type="radio"
          name="lessonType"
          id="1"
          value="1"
          onChange={onTypeChanged}
          checked={settings.type === 1}
        />
        <label htmlFor="1">Fiszki</label>
        <input
          type="radio"
          name="lessonType"
          id="2"
          value="2"
          onChange={onTypeChanged}
          checked={settings.type === 2}
        />
        <label htmlFor="2">Typing</label>
      </form>
      <br />
    </>
  );
}

function filterGroups(groups: Group[], language: number): Group[] {
  if (language === 1) {
    return groups.filter((x) => x.backCount > 0);
  }
  if (language === 2) {
    return groups.filter((x) => x.frontCount > 0);
  }
  return groups;
}
