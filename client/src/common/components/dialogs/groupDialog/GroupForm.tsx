import "./GroupForm.scss";
import "../forms.scss";
import { useFormik } from "formik";
import { Dropdown } from "primereact/dropdown";
import { ReactElement } from "react";
import GroupDetails from "./groupDetails";

export default function GroupForm({ group, options, onSubmit }: Model): ReactElement {
  const onsubmit = (values: FormModel) => {
    const updated = group ? { ...group } : ({} as GroupDetails);
    updated.name = values.name;
    updated.front = values.front;
    updated.back = values.back;
    onSubmit(updated);
  };

  const formik = useFormik({
    initialValues: {
      name: group?.name ?? "",
      front: group?.front ?? "",
      back: group?.back ?? "",
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
          options={options}
          editable={true}
          onChange={(e) => formik.setFieldValue("front", e.value)}
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          placeholder="Select language..."
        />
      </div>
      <div className="dialog-form-item">
        <label className="input-label">Back Language</label>
        <Dropdown
          value={formik.values.back}
          options={options}
          editable={true}
          onChange={(e) => formik.setFieldValue("back", e.value)}
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
  front: string;
  back: string;
}

interface Model {
  group: GroupDetails;
  options: string[];
  onSubmit: (group: GroupDetails) => void;
}

const dropdownItemLayout = (option: string, props: any = null) => {
  return option ? (
    <div className="language-options-item">{option}</div>
  ) : (
    <span>{props.placeholder}</span>
  );
};
