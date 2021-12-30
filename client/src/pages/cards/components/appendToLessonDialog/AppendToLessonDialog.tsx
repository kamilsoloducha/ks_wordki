import { useFormik } from "formik";
import { Dialog } from "primereact/dialog";
import { ReactElement } from "react";

export default function AppendToLessonDialog({
  isVisible,
  onSubmit,
  onHide,
}: Model): ReactElement {
  const formik = useFormik({
    initialValues,
    onSubmit: (values) => onSubmit(values.count, values.languages),
  });

  const footer = (
    <>
      <button onClick={onHide}>Cancel</button>
      <button
        onClick={() => onSubmit(formik.values.count, formik.values.languages)}
      >
        Save
      </button>
    </>
  );

  return (
    <Dialog
      onHide={onHide}
      visible={isVisible}
      footer={footer}
      draggable={false}
      dismissableMask={false}
    >
      <form>
        <div className="form-item">
          <label className="form-label">Cards to append</label>
          <input
            id="count"
            name="count"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.count}
          />
        </div>
        <div className="form-item">
          <label className="form-label">Languages</label>
          <input
            id="languages"
            name="languages"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.languages}
          />
        </div>
      </form>
    </Dialog>
  );
}

export interface Model {
  isVisible: boolean;
  onSubmit: (count: number, languages: number) => void;
  onHide: () => void;
}

interface FormModel {
  count: number;
  languages: number;
}

const initialValues: FormModel = { count: 30, languages: 3 };
