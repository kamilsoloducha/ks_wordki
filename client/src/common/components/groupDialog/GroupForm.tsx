import "./GroupForm.scss";
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
      front: group?.front,
      back: group?.back,
    },
    onSubmit: onsubmit,
  });

  const options = Languages;

  const languageOptions = (option: Language) => {
    return option ? (
      <div className="language-options-item">
        <img className="flag" src={option.icon} width="24px" />
        {option.label}
      </div>
    ) : (
      <div></div>
    );
  };

  return (
    <form id="form" onSubmit={formik.handleSubmit} autoComplete="off">
      <div>
        <label>Name</label>
        <input
          id="name"
          name="name"
          type="text"
          onChange={formik.handleChange}
          value={formik.values.name}
        />
      </div>
      <div>
        <Dropdown
          value={formik.values.front}
          options={options}
          onChange={(e) => formik.setFieldValue("front", e.value)}
          optionLabel="label"
          optionValue="type"
          itemTemplate={languageOptions}
          valueTemplate={languageOptions}
        />
      </div>
      <div>
        <Dropdown
          value={formik.values.back}
          options={options}
          onChange={(e) => formik.setFieldValue("back", e.value)}
          optionLabel="label"
          optionValue="type"
          itemTemplate={languageOptions}
          valueTemplate={languageOptions}
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
