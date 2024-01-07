import "./ActionsDialog.scss";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";

export default function ActionsDialog({ isVisible, onHide, actions }: Model): ReactElement {
  const onButtonClick = (action: () => void) => {
    action();
    onHide();
  };

  const dialogContent = (
    <div className="actions-dialog-container">
      {actions.map((item) => (
        <button
          className="actions-dialog-item clickable"
          key={item.label}
          onClick={() => {
            onButtonClick(item.action);
          }}
        >
          {item.label}
        </button>
      ))}
      <button className="actions-dialog-item clickable" onClick={onHide}>
        Cancel
      </button>
    </div>
  );

  return (
    <Dialog
      content={dialogContent}
      showHeader={false}
      visible={isVisible}
      onHide={onHide}
      style={{ padding: "none" }}
    ></Dialog>
  );
}

interface Model {
  isVisible: boolean;
  onHide: () => void;
  actions: Action[];
}

interface Action {
  label: string;
  action: () => void;
}
