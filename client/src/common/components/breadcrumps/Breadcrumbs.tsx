import { ReactElement } from "react";
import { Link } from "react-router-dom";
import "./Breadcrumbs.scss";

export default function Breadcrubms({ items }: Model): ReactElement {
  return (
    <div className="breadcrumbs-container">
      {items.map((item) => (
        <Link to={item.path}>{item.label}</Link>
      ))}
    </div>
  );
}

interface Model {
  items: Item[];
}

interface Item {
  label: string;
  path: string;
}
