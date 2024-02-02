import { ReactElement } from "react";

export function TabViewItemHeader({
  header,
  isSelected,
  value,
  onClick,
}: TabViewItemHeaderProps): ReactElement {
  return (
    <div
      className={`tab-view-header-item + ${isSelected ? "selected" : ""}`}
      onClick={() => onClick(value)}
    >
      {header}
    </div>
  );
}

export type TabViewItemHeaderProps = {
  header: string;
  isSelected: boolean;
  value: number;
  onClick: (value: number) => void;
};
