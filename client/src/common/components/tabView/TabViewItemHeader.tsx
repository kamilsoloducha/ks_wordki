import { ReactElement } from "react";

export function TabViewItemHeader({ header, isSelected, value, onClick }: Model): ReactElement {
  return (
    <div
      className={`tab-view-header-item + ${isSelected ? "selected" : ""}`}
      onClick={() => onClick(value)}
    >
      {header}
    </div>
  );
}

interface Model {
  header: string;
  isSelected: boolean;
  value: number;
  onClick: (value: number) => void;
}
