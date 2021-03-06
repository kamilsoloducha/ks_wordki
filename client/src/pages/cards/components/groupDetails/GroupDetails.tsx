import "./GroupDetails.scss";

export function GroupDetails({ name, onSettingsClick }: Model) {
  return (
    <div className="group-details-container">
      <div className="group-details-name">{name}</div>
      <div className="group-details-settings clickable">
        <img onClick={onSettingsClick} src="/svgs/settings.svg" alt="set" />
      </div>
    </div>
  );
}

interface Model {
  name: string;
  front: number;
  back: number;
  onSettingsClick: () => void;
}
