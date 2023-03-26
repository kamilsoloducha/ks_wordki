import { Language } from "pages/lessonSettings/models/languages";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";
import Footer from "../cardDialog/Footer";
import GroupDetails from "./groupDetails";
import GroupForm from "./GroupForm";

export default function GroupDialog({
  cardSides,
  group,
  onHide,
  onSubmit,
  onDelete,
}: Model): ReactElement {
  const ondelete: () => void = () => {
    if (onDelete) onDelete(group);
  };

  const footer = <Footer onhide={onHide} ondelete={ondelete} />;

  return (
    <Dialog
      style={{ width: "50vw" }}
      footer={footer}
      visible={group !== null}
      onHide={onHide}
      header={group?.id ? "Editing Group" : "Creating Group"}
      draggable={false}
      dismissableMask={true}
    >
      <GroupForm options={cardSides.map((x) => x.language)} group={group} onSubmit={onSubmit} />
    </Dialog>
  );
}

interface Model {
  group: GroupDetails;
  cardSides: Language[];
  onHide: () => void;
  onSubmit: (item: GroupDetails) => void;
  onDelete?: (item: GroupDetails) => void;
}
