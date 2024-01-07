import "./CardDialog.scss";
import { ReactElement } from "react";
import { Dialog } from "primereact/dialog";
import Footer from "./Footer";
import CardForm, { FormModel } from "./CardForm";
import Language from "common/models/languages";

export default function CardDialog({
  card,
  onHide,
  onSubmit,
  onDelete,
  frontLanguage,
  backLanguage,
}: Model): ReactElement {
  const visible = card !== null;
  const isEditing = card?.cardId;
  const header = isEditing ? "Editing Card" : "Creating Card";

  const ondelete: () => void = () => {
    if (!card) return;
    if (onDelete) onDelete(card);
  };

  const footer = <Footer onhide={onHide} ondelete={isEditing ? ondelete : undefined} />;
  const content = (
    <CardForm
      card={card}
      onSubmit={onSubmit}
      frontLanguage={frontLanguage}
      backLanguage={backLanguage}
    />
  );
  return (
    <Dialog
      content={content}
      footer={footer}
      visible={visible}
      onHide={onHide}
      header={header}
      draggable={false}
      dismissableMask={false}
    ></Dialog>
  );
}

interface Model {
  card: FormModel | null;
  onHide: () => void;
  onSubmit: (item: FormModel) => void;
  onDelete?: (item: FormModel) => void;
  frontLanguage?: Language;
  backLanguage?: Language;
}
