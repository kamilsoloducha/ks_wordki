import { useFormik } from 'formik'
import { useCallback, useEffect, useRef } from 'react'
import Language from 'common/models/languages'

export function CardForm({ card, onSubmit, frontLanguage, backLanguage }: CardFormProps) {
  const firstInputRef = useRef<HTMLInputElement>(null)

  const onsubmit = useCallback(
    (values: CardFormModel) => {
      onSubmit(values)
      if (firstInputRef.current) firstInputRef.current.focus()
    },
    [onSubmit]
  )

  const formik = useFormik({
    initialValues: card as CardFormModel,
    onSubmit: (values) => onsubmit(values),
    enableReinitialize: true
  })

  useEffect(() => {
    formik.resetForm()
  }, [card]) // eslint-disable-line

  if (!card) return <></>

  return (
    <>
      <form
        id="card-form"
        className="w-full  border-y-2 border-accent-dark bg-main"
        onSubmit={formik.handleSubmit}
        autoComplete="off"
      >
        <div className="relative flex flex-col py-3">
          <label htmlFor="frontValue" className="ps-3">
            {frontLanguage?.label}
          </label>
          {frontLanguage && frontLanguage?.icon && (
            <img
              className="absolute left-5 top-8"
              src={frontLanguage?.icon}
              width="32px"
              alt={frontLanguage.label}
            />
          )}
          <input
            className="border-b-2 border-accent-super-light bg-transparent text-center text-3xl font-extrabold text-accent-super-light"
            ref={firstInputRef}
            id="frontValue"
            name="frontValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontValue}
          />
        </div>

        <div className="relative flex flex-col py-3">
          <label htmlFor="backValue" className="ps-3">
            {backLanguage?.label}
          </label>
          {backLanguage && (
            <img
              className="absolute left-5 top-8"
              src={backLanguage?.icon}
              width="32px"
              alt={backLanguage.label}
            />
          )}
          <input
            className="border-b-2 border-accent-super-light bg-transparent text-center text-3xl font-extrabold text-accent-super-light"
            id="backValue"
            name="backValue"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backValue}
          />
        </div>

        <div className="relative flex flex-col py-3">
          <label htmlFor="frontExample" className="ps-3">
            {frontLanguage?.label} example
          </label>
          {frontLanguage && frontLanguage?.icon && (
            <img
              className="absolute left-5 top-8"
              src={frontLanguage?.icon}
              width="32px"
              alt={frontLanguage.label}
            />
          )}
          <input
            className="border-b-2 border-accent-super-light bg-transparent text-center text-3xl font-extrabold text-accent-super-light"
            id="frontExample"
            name="frontExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.frontExample}
          />
        </div>

        <div className="relative flex flex-col py-3">
          {backLanguage && (
            <img
              className="absolute left-5 top-8"
              src={backLanguage?.icon}
              width="32px"
              alt={backLanguage.label}
            />
          )}
          <label htmlFor="backExample" className="ps-3">
            {backLanguage?.label} example
          </label>
          <input
            className="border-b-2 border-accent-super-light bg-transparent text-center text-3xl font-extrabold text-accent-super-light"
            id="backExample"
            name="backExample"
            autoComplete="off"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.backExample}
          />
        </div>

        <div className="flex py-3">
          <label htmlFor="frontEnabled" className="ps-3">
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

        <div className="flex  py-3">
          <label htmlFor="backEnabled" className="ps-3">
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

        <div className="flex flex-col py-3">
          <label htmlFor="comment" className="ps-3">
            Comment
          </label>
          <input
            className="border-b-2 border-accent-super-light bg-transparent text-center text-3xl font-extrabold text-accent-super-light"
            id="comment"
            name="comment"
            type="text"
            autoComplete="off"
            onChange={formik.handleChange}
            value={formik.values.comment}
          />
        </div>

        <div className="flex py-3">
          <label htmlFor="isTicked" className="ps-3">
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
  )
}

export type CardFormModel = {
  cardId?: string
  frontValue: string
  frontExample: string
  frontEnabled: any
  backValue: string
  backExample: string
  backEnabled: any
  comment: string
  isTicked: any
}

type CardFormProps = {
  card?: CardFormModel
  onSubmit: (item: CardFormModel) => void
  frontLanguage?: Language
  backLanguage?: Language
}
