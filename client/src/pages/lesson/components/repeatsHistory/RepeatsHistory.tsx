import "./RepeatsHistory.scss";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";
import UserRepeat from "pages/lesson/models/userRepeat";
import { History } from "../history/History";

export function RepeatHistory({ visible, onHide, history }: Model): ReactElement {
  const dialogContent = <History history={history} />;
  return (
    <Dialog content={dialogContent} onHide={onHide} visible={visible} draggable={false}></Dialog>
  );
}

interface Model {
  visible: boolean;
  onHide: () => void;
  history: UserRepeat[];
}
