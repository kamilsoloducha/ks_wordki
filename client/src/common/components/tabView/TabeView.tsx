import "./TabView.scss";
import { ReactElement } from "react";
import { TabViewItemHeader } from "./TabViewItemHeader";

export function TabView({ selectedValue, items, onItemChanged }: Model): ReactElement {
  const content = items.find((x) => x.value === selectedValue)?.element;

  const onClick = (value: number) => {
    if (onItemChanged) onItemChanged(value);
  };

  return (
    <>
      <div className="tab-view-header-container">
        {items.map((item, index) => (
          <TabViewItemHeader
            key={index}
            header={item.header}
            isSelected={selectedValue === item.value}
            value={index}
            onClick={onClick}
          />
        ))}
      </div>
      <div className="tab-view-body-container">{content}</div>
    </>
  );
}

export interface TabViewItemModel {
  header: string;
  element: ReactElement;
  value: number;
}

interface Model {
  selectedValue: number;
  items: TabViewItemModel[];
  onItemChanged?: (value: number) => void;
}
