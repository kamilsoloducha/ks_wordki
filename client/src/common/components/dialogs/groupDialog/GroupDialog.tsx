import "../dialogs.scss";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";
import Footer from "../cardDialog/Footer";
import GroupDetails from "./groupDetails";
import GroupForm from "./GroupForm";

export default function GroupDialog({ group, onHide, onSubmit, onDelete }: Model): ReactElement {
  const visible = group !== null;
  const isEditing = group?.id;
  const header = isEditing ? "Editing Group" : "Creating Group";

  const ondelete: () => void = () => {
    if (onDelete) onDelete(group);
  };
  const footer = <Footer onhide={onHide} ondelete={isEditing ? ondelete : undefined} />;

  return (
    <Dialog
      style={{ width: "50vw" }}
      footer={footer}
      visible={visible}
      onHide={onHide}
      header={header}
      draggable={false}
      dismissableMask={true}
    >
      <GroupForm group={group} onSubmit={onSubmit} />
    </Dialog>
  );
}

interface Model {
  group: GroupDetails;
  onHide: () => void;
  onSubmit: (item: GroupDetails) => void;
  onDelete?: (item: GroupDetails) => void;
}
