import "../forms.scss";
import "./CardForm.scss";
import { useFormik } from "formik";
import { useCallback, useEffect, useRef } from "react";
import Language from "common/models/languages";

export default function CardForm({ card, onSubmit, frontLanguage, backLanguage }: Model) {
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
  }, [card]); // eslint-disable-line

  if (!card) return <></>;

  return (
    <>
      <form
        id="group-dialog-form"
        className="dialog-form"
        onSubmit={formik.handleSubmit}
        autoComplete="off"
      >
        <div className="dialog-form-item">
          <label htmlFor="frontValue" className="input-label">
            Front value
          </label>
          {frontLanguage && (
            <img
              className="dialog-input-icon"
              src={frontLanguage?.icon}
              width="24px"
              alt={frontLanguage.label}
            />
          )}
          <input
            className="form-text-input"
            ref={firstInputRef}
            id="frontValue"
            name="frontValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontValue}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="backValue" className="input-label">
            Back value
          </label>
          {backLanguage && (
            <img
              className="dialog-input-icon"
              src={backLanguage?.icon}
              width="24px"
              alt={backLanguage.label}
            />
          )}
          <input
            className="form-text-input"
            id="backValue"
            name="backValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backValue}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="frontExample" className="input-label">
            Front example
          </label>
          {frontLanguage && (
            <img
              className="dialog-input-icon"
              src={frontLanguage?.icon}
              width="24px"
              alt={frontLanguage.label}
            />
          )}
          <input
            className="form-text-input"
            id="frontExample"
            name="frontExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontExample}
          />
        </div>

        <div className="dialog-form-item">
          {backLanguage && (
            <img
              className="dialog-input-icon"
              src={backLanguage?.icon}
              width="24px"
              alt={backLanguage.label}
            />
          )}
          <label htmlFor="backExample" className="input-label">
            Back example
          </label>
          <input
            className="form-text-input"
            id="backExample"
            name="backExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backExample}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="frontEnabled" className="checkbox-label">
            Front used
          </label>
          <input
            id="frontEnabled"
            name="frontEnabled"
            type="checkbox"
            onChange={formik.handleChange}
            checked={formik.values.frontEnabled}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="backEnabled" className="checkbox-label">
            Back used
          </label>
          <input
            id="backEnabled"
            name="backEnabled"
            type="checkbox"
            onChange={formik.handleChange}
            checked={formik.values.backEnabled}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="comment" className="input-label">
            Comment
          </label>
          <input
            className="form-text-input"
            id="comment"
            name="comment"
            type="text"
            autoComplete="off"
            onChange={formik.handleChange}
            value={formik.values.comment}
          />
        </div>

        <div className="dialog-form-item">
          <label htmlFor="isTicked" className="checkbox-label">
            Is Ticked
          </label>
          <input
            id="isTicked"
            name="isTicked"
            type="checkbox"
            onChange={formik.handleChange}
            checked={formik.values.isTicked}
          />
        </div>
      </form>
    </>
  );
}

export interface FormModel {
  cardId: number;
  frontValue: string;
  frontExample: string;
  frontEnabled: any;
  backValue: string;
  backExample: string;
  backEnabled: any;
  comment: string;
  isTicked: any;
}

interface Model {
  card: FormModel | null;
  onSubmit: (item: FormModel) => void;
  frontLanguage?: Language;
  backLanguage?: Language;
}
