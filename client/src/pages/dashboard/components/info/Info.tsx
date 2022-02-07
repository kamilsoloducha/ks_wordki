import "./Info.scss";

export function Info({ title, value, onClick }: Model) {
  return (
    <div className="info-container" onClick={onClick}>
      <div className="info-title">{title}</div>
      <div className="info-value">{value}</div>
    </div>
  );
}

interface Model {
  title: string;
  value: string | number;
  onClick?: () => void;
}
