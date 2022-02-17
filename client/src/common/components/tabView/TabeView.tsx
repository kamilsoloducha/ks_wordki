import "./TabView.scss";
import { ReactElement, useState } from "react";
import { TabViewItemHeader } from "./TabViewItemHeader";

export function TabView({ items, onItemChanged }: Model): ReactElement {
  const [selected, setSelected] = useState(0);

  const onClick = (value: number) => {
    setSelected(value);
    if (onItemChanged) onItemChanged(value);
  };

  return (
    <>
      <div className="tab-view-header-container">
        {items.map((item, index) => (
          <TabViewItemHeader
            key={index}
            header={item.header}
            isSelected={selected === index}
            value={index}
            onClick={onClick}
          />
        ))}
      </div>
      <div className="tab-view-body-container">{items[selected].element}</div>
    </>
  );
}

export interface TabViewItemModel {
  header: string;
  element: ReactElement;
}

interface Model {
  items: TabViewItemModel[];
  onItemChanged?: (value: number) => void;
}
