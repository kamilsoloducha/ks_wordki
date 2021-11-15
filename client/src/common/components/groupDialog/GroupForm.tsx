import { Field, Form, Formik } from "formik";
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

  return (
    <Formik
      initialValues={
        {
          name: group?.name ?? "",
          front: group?.front ?? 0,
          back: group?.back ?? 0,
        } as FormModel
      }
      onSubmit={onsubmit}
    >
      <Form id="form">
        <div>
          <label>Group name</label>
          <Field id="name" name="name" autoComplete="off" />
          <Field as="select" id="front" name="front">
            <option key={0} value={0}>
              {0}
            </option>
            <option key={1} value={1}>
              {1}
            </option>
            <option key={2} value={2}>
              {2}
            </option>
          </Field>
        </div>
      </Form>
    </Formik>
  );
}

interface FormModel {
  name: string;
  front: number;
  back: number;
}

interface Model {
  group: GroupDetails;
  onSubmit: (group: GroupDetails) => void;
}
