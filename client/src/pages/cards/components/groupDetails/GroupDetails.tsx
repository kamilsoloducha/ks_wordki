import "./GroupDetails.scss";

function GroupDetails({ name }: Model) {
  return (
    <div className="group-details-container">
      <div className="group-details-name">{name}</div>
    </div>
  );
}

export default GroupDetails;

interface Model {
  name: string;
  front: number;
  back: number;
}
