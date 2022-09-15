import "./GroupForm.scss";
import "../forms.scss";
import Language, { Languages } from "common/models/languages";
import { useFormik } from "formik";
import { Dropdown } from "primereact/dropdown";
import { ReactElement } from "react";
import GroupDetails from "./groupDetails";

export default function GroupForm({ group, onSubmit }: Model): ReactElement {
  const onsubmit = (values: FormModel) => {
    const updated = !group ? ({} as GroupDetails) : { ...group };
    updated.name = values.name;
    updated.front = values.front;
    updated.back = values.back;
    onSubmit(updated);
  };

  const formik = useFormik({
    initialValues: {
      name: group?.name ?? "",
      front: group?.front ?? 0,
      back: group?.back ?? 0,
    },
    onSubmit: onsubmit,
  });

  return (
    <form
      id="group-dialog-form"
      className="dialog-form"
      onSubmit={formik.handleSubmit}
      autoComplete="off"
    >
      <div className="dialog-form-item">
        <label htmlFor="name" className="input-label">
          Name
        </label>
        <input
          id="name"
          name="name"
          type="text"
          onChange={formik.handleChange}
          value={formik.values.name}
        />
      </div>
      <div className="dialog-form-item">
        <label className="input-label">Front Language</label>
        <Dropdown
          value={formik.values.front}
          options={Languages}
          onChange={(e) => formik.setFieldValue("front", e.value)}
          optionLabel="label"
          optionValue="type"
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          placeholder="Select language..."
        />
      </div>
      <div className="dialog-form-item">
        <label className="input-label">Back Language</label>
        <Dropdown
          value={formik.values.back}
          options={Languages}
          onChange={(e) => formik.setFieldValue("back", e.value)}
          optionLabel="label"
          optionValue="type"
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          placeholder="Select language..."
        />
      </div>
    </form>
  );
}

interface FormModel {
  name: string;
  front: any;
  back: number;
}

interface Model {
  group: GroupDetails;
  onSubmit: (group: GroupDetails) => void;
}

const dropdownItemLayout = (option: Language, props: any = null) => {
  return option ? (
    <div className="language-options-item">
      <img className="flag" src={option.icon} width="24px" alt={option.label} />
      {option.label}
    </div>
  ) : (
    <span>{props.placeholder}</span>
  );
};
