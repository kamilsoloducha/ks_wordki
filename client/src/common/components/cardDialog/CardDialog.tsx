import "./CardDialog.scss";
import { ReactElement } from "react";
import { Dialog } from "primereact/dialog";
import { CardSummary } from "pages/cards/models/cardSummary";
import Footer from "./Footer";
import CardForm from "./CardForm";

export default function CardDialog({
  card,
  onHide,
  onSubmit,
  onDelete,
}: Model): ReactElement {
  const visible = card !== null;
  const isEditing = card?.id;
  const header = isEditing ? "Editing Card" : "Creating Card";

  const ondelete: () => void = () => {
    if (onDelete) onDelete(card);
  };

  const footer = (
    <Footer onhide={onHide} ondelete={isEditing ? ondelete : undefined} />
  );

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
      <CardForm card={card} onSubmit={onSubmit} />
    </Dialog>
  );
}

interface Model {
  card: CardSummary | null;
  onHide: () => void;
  onSubmit: (item: CardSummary) => void;
  onDelete?: (item: CardSummary | null) => void;
}
