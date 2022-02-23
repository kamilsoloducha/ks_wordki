import "./Footer.scss";
import { ReactElement } from "react";

export default function Footer({ onhide, ondelete }: FooterModel): ReactElement {
  return (
    <div className="card-dialog-footer">
      {ondelete && (
        <button className="float-left delete" onClick={ondelete}>
          Delete
        </button>
      )}
      <button type="submit" form="group-dialog-form" className="float-right save">
        Save
      </button>
      <button className="float-right cancel" onClick={onhide}>
        Cancel
      </button>
    </div>
  );
}

interface FooterModel {
  onhide: () => void;
  ondelete?: () => void;
}
