import { Field, Form, Formik } from "formik";
import { CardSummary } from "pages/cards/models/groupDetailsSummary";

export default function CardForm({ card, onSubmit }: Model) {
  const onsubmit = (values: FormModel) => {
    const updated = !card
      ? ({ back: {}, front: {} } as CardSummary)
      : { ...card };

    updated.front.value = values.frontValue;
    updated.front.example = values.frontExample;
    updated.front.isUsed = values.frontEnabled;

    updated.back.value = values.backValue;
    updated.back.example = values.backExample;
    updated.back.isUsed = values.backEnabled;

    updated.comment = values.comment;
    onSubmit(updated);
  };

  return (
    <Formik
      initialValues={
        {
          frontValue: card?.front.value ?? "",
          frontExample: card?.front.example ?? "",
          frontEnabled: card?.front.isUsed,
          backValue: card?.back.value ?? "",
          backExample: card?.back.example ?? "",
          backEnabled: card?.back.isUsed,
          comment: card?.comment ?? "",
        } as FormModel
      }
      onSubmit={onsubmit}
      enableReinitialize={true}
    >
      <Form id="form">
        <div>
          <label>Front value</label>
          <Field id="frontValue" name="frontValue" autoComplete="off" />
        </div>
        <div>
          <label>Back value</label>
          <Field id="backValue" name="backValue" autoComplete="off" />
        </div>
        <div>
          <label>Front example</label>
          <Field id="frontExample" name="frontExample" autoComplete="off" />
        </div>
        <div>
          <label>Back example</label>
          <Field id="backExample" name="backExample" autoComplete="off" />
        </div>
        <div>
          <label>Front used</label>
          <Field type="checkbox" name="frontEnabled" autoComplete="off" />
        </div>
        <div>
          <label>Back used</label>
          <Field type="checkbox" name="backEnabled" autoComplete="off" />
        </div>
        <div>
          <label>Comment</label>
          <Field id="comment" name="comment" autoComplete="off" />
        </div>
      </Form>
    </Formik>
  );
}

interface FormModel {
  frontValue: string;
  frontExample: string;
  frontEnabled: boolean;
  backValue: string;
  backExample: string;
  backEnabled: boolean;
  comment: string;
}

interface Model {
  card: CardSummary | null;
  onSubmit: (item: CardSummary) => void;
}
