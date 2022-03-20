import "./LessonInformation.scss";
import * as sel from "store/lesson/selectors";
import * as actions from "store/lesson/actions";
import { ReactElement, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Menu } from "primereact/menu";
import { Button } from "primereact/button";
import { RepeatHistory } from "../repeatsHistory/RepeatsHistory";

export function LessonInformation(): ReactElement {
  const dispatch = useDispatch();
  const [repeatsPopup, setRepeatsPopup] = useState(false);
  const menu = useRef<Menu>(null);
  const questions = useSelector(sel.selectRepeats);
  const results = useSelector(sel.selectResults);
  const repeatsHistory = useSelector(sel.selectLessonHistory);
  const status = useSelector(sel.selectLessonState);

  const hideHistory = () => {
    setRepeatsPopup(false);
  };

  const items = [
    {
      label: "Show History",
      command: () => setRepeatsPopup(true),
    },
    {
      label: "Tick card",
      command: () => dispatch(actions.tickCard()),
    },
  ];

  return (
    <div className="lesson-information">
      <div className="lesson-information-item">
        <div className="label">Remained</div>
        <div className="value">{questions.length}</div>
      </div>
      <div className="lesson-information-item">
        <div className="label">Counter</div>
        <div className="value">{results.answers ?? 0}</div>
      </div>
      <div className="lesson-information-item">
        <div className="label">Correct</div>
        <div className="value correct">{results.correct ?? 0}</div>
      </div>
      <div className="lesson-information-item">
        <div className="label">Accepted</div>
        <div className="value accepted">{results.accept ?? 0}</div>
      </div>
      <div className="lesson-information-item">
        <div className="label">Wrong</div>
        <div className="value wrong">{results.wrong ?? 0}</div>
      </div>
      <div>
        <Menu model={items} popup ref={menu} id="popup_menu" />
        <Button
          disabled={!status.btnPause}
          icon="pi pi-bars"
          onClick={(event) => menu.current?.toggle(event)}
          aria-controls="popup_menu"
          aria-haspopup
        />
      </div>
      <RepeatHistory
        visible={repeatsPopup}
        onHide={hideHistory}
        history={[...repeatsHistory].reverse()}
      />
    </div>
  );
}
