import { Group } from "pages/lessonSettings/models/group";
import { Dropdown } from "primereact/dropdown";
import { ReactElement } from "react";
import "./GroupSelector.scss";

export function GroupSelector({ items, selectedGroupId, onSelectedChanged }: Model): ReactElement {
  const selectedGroup = items.find((x) => x.id === selectedGroupId);

  return (
    <div className="group-selector-container">
      <p>Selected group:</p>
      <Dropdown
        value={selectedGroup}
        options={items}
        onChange={(event$) => onSelectedChanged(event$.value.id)}
        itemTemplate={dropdownItemLayout}
        valueTemplate={dropdownItemLayout}
        optionLabel="name"
        placeholder="Select group..."
      />
    </div>
  );
}

interface Model {
  items: Group[];
  selectedGroupId: string | null;
  onSelectedChanged: (groupId: string) => void;
}

const dropdownItemLayout = (option: Group, props: any = null) => {
  if (option) {
    return (
      <div className="group-item">
        <strong>{option.name}</strong>
        {flagLayout(option.front, option.frontCount)}
        {flagLayout(option.back, option.backCount)}
      </div>
    );
  }
  return (
    <>
      <span>{props.placeholder}</span>
    </>
  );
};

const flagLayout = (lang: string, count: number) => {
  return (
    <>
      ({lang})<strong>({count})</strong>
    </>
  );
};
