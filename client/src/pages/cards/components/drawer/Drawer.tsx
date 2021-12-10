import { ReactElement } from "react";
import "./Drawer.scss";

export default function Drawer(model: Model): ReactElement {
  const colorClass = getColorClass(model);

  const onClick = (event$: any) => {
    if (!model.onClick) return;

    event$.stopPropagation();
    model.onClick();
  };

  return <div className={`${colorClass} drawer`} onClick={onClick}></div>;
}

interface Model {
  isUsed: boolean;
  drawer: number;
  onClick?: () => void;
}

function getColorClass({ isUsed, drawer }: Model): string {
  return isUsed ? "info-card-drawer-" + drawer : "info-card-gray";
}
