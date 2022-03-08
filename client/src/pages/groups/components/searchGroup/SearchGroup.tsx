import "./SearchGroup.scss";
import { useFormik } from "formik";
import { Dialog } from "primereact/dialog";
import React, { ReactElement } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as actions from "store/groups/actions";
import * as selectors from "store/groups/selectors";
import GroupRow from "../groupRow/GroupRow";

export function SearchGroup({ visible, onHide }: Model): ReactElement {
  const dispatch = useDispatch();
  const groups = useSelector(selectors.selectSearchingItems);

  const submitForm = (model: FormModel) => {
    dispatch(actions.searchGroup(model.groupName));
  };

  const formik = useFormik({
    initialValues: {
      groupName: "",
    },
    onSubmit: submitForm,
  });

  return (
    <Dialog
      style={{ width: "50vw" }}
      visible={visible}
      onHide={onHide}
      draggable={false}
      dismissableMask={true}
      modal={true}
    >
      <form id="seachGroupForm" autoComplete="off" onSubmit={(e) => e.preventDefault()}>
        <div>
          <label htmlFor="groupName">Name</label>
          <input
            id="groupName"
            name="groupName"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.groupName}
          />
        </div>
        <button onClick={() => submitForm(formik.values)}>Search</button>
      </form>
      <div>
        {groups.map((x) => (
          <React.Fragment key={x.id}>
            <GroupRow
              groupSummary={{
                id: x.id,
                name: x.name,
                front: x.front,
                back: x.back,
                cardsCount: x.cardsCount,
                cardsEnabled: x.cardsEnabled ?? 0,
              }}
            />
            <hr style={{ backgroundColor: "rgb(133, 133, 133)", height: "1px", border: "none" }} />
          </React.Fragment>
        ))}
      </div>
    </Dialog>
  );
}

interface Model {
  visible: boolean;
  onHide(): void;
}

interface FormModel {
  groupName: string;
}
