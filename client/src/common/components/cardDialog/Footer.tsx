import { ReactElement } from "react";

export default function Footer({
  onhide,
  ondelete,
}: FooterModel): ReactElement {
  return (
    <div className="card-dialog-footer">
      {ondelete && (
        <button className="float-left" onClick={ondelete}>
          Delete
        </button>
      )}
      <button type="submit" form="form" className="float-right">
        Save
      </button>
      <button className="float-right" onClick={onhide}>
        Cancel
      </button>
    </div>
  );
}

interface FooterModel {
  onhide: () => void;
  ondelete?: () => void;
}
