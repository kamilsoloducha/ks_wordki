import { useFormik } from "formik";
import { useCallback, useEffect, useRef } from "react";

export default function CardForm({ card, onSubmit }: Model) {
  const firstInputRef = useRef<any>(null);

  const onsubmit = useCallback(
    (values: FormModel) => {
      onSubmit(values);
      if (firstInputRef.current) firstInputRef.current.focus();
    },
    [onSubmit]
  );

  const formik = useFormik({
    initialValues: card as FormModel,
    onSubmit: (values) => onsubmit(values),
    enableReinitialize: true,
  });

  useEffect(() => {
    formik.resetForm();
  }, [card, formik]);

  if (!card) return <></>;

  return (
    <>
      <form id="form" onSubmit={formik.handleSubmit} autoComplete="off">
        <div>
          <label>Front value</label>
          <input
            ref={firstInputRef}
            id="frontValue"
            name="frontValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontValue}
          />
        </div>

        <div>
          <label>Back value</label>
          <input
            id="backValue"
            name="backValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backValue}
          />
        </div>

        <div>
          <label>Front example</label>
          <input
            id="frontExample"
            name="frontExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontExample}
          />
        </div>

        <div>
          <label>Back example</label>
          <input
            id="backExample"
            name="backExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backExample}
          />
        </div>

        <div>
          <label>Front used</label>
          <input
            id="frontEnabled"
            name="frontEnabled"
            type="checkbox"
            onChange={formik.handleChange}
            checked={formik.values.frontEnabled}
          />
        </div>

        <div>
          <label>Back used</label>
          <input
            id="backEnabled"
            name="backEnabled"
            type="checkbox"
            onChange={formik.handleChange}
            checked={formik.values.backEnabled}
          />
        </div>

        <div>
          <label>Comment</label>
          <input
            id="comment"
            name="comment"
            type="text"
            autoComplete="off"
            onChange={formik.handleChange}
            value={formik.values.comment}
          />
        </div>
      </form>
    </>
  );
}

export interface FormModel {
  cardId: string;
  frontValue: string;
  frontExample: string;
  frontEnabled: any;
  backValue: string;
  backExample: string;
  backEnabled: any;
  comment: string;
}

interface Model {
  card: FormModel | null;
  onSubmit: (item: FormModel) => void;
}
