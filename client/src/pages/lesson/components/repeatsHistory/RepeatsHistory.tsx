import "./RepeatsHistory.scss";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";
import UserRepeat from "pages/lesson/models/userRepeat";
import { History } from "../history/History";

export function RepeatHistory({ visible, onHide, history }: Model): ReactElement {
  return (
    <Dialog onHide={onHide} visible={visible} draggable={false}>
      <History history={history} />
    </Dialog>
  );
}

interface Model {
  visible: boolean;
  onHide: () => void;
  history: UserRepeat[];
}
