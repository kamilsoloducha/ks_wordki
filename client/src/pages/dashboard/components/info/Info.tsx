import "./Info.scss";

function Info({ title, value, onClick }: Model) {
  return (
    <div className="info-container" onClick={onClick}>
      <div className="info-title">{title}</div>
      <div className="info-value">{value}</div>
    </div>
  );
}

export default Info;

interface Model {
  title: string;
  value: string | number;
  onClick?: () => void;
}
