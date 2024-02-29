import { useFormik } from 'formik'
import { Dropdown } from 'primereact/dropdown'
import { ReactElement, ReactNode } from 'react'

export function GroupForm({ group, options, onSubmit }: GroupFormProps): ReactElement {
  const formik = useFormik({
    initialValues: {
      id: group?.id,
      name: group?.name ?? '',
      front: group?.front ?? '',
      back: group?.back ?? ''
    },
    onSubmit: (value) => onSubmit(value),
    validate: validate
  })

  const onChange = (id: string, value: string) => {
    formik.setFieldValue(id, value, true)
    formik.setFieldTouched(id, true, false)
  }

  return (
    <form
      className="w-full  border-y-2 border-accent-dark bg-main"
      onSubmit={formik.submitForm}
      autoComplete="off"
    >
      <div className="flex flex-col py-3">
        <label htmlFor="name" className="ps-3">
          Name
        </label>
        <input
          className="bg-transparent text-center border-b-2 border-accent-super-light text-accent-super-light font-extrabold text-3xl"
          id="name"
          name="name"
          type="text"
          onChange={(e) => onChange(e.target.id, e.target.value)}
          value={formik.values.name}
        />
        {formik.errors.name && formik.touched.name && (
          <ValidationError error={formik.errors.name} />
        )}
      </div>
      <div className="flex flex-col py-3">
        <label className="ps-3">Front Language</label>
        <Dropdown
          id="front"
          value={formik.values.front}
          options={options}
          editable={true}
          onChange={(e) => onChange(e.target.id, e.value)}
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          placeholder="Select language..."
        />
        {formik.errors.front && formik.touched.front && (
          <ValidationError error={formik.errors.front} />
        )}
      </div>
      <div className="flex flex-col py-3">
        <label className="ps-3">Back Language</label>
        <Dropdown
          id="back"
          value={formik.values.back}
          options={options}
          editable={true}
          onChange={(e) => onChange(e.target.id, e.value)}
          itemTemplate={dropdownItemLayout}
          valueTemplate={dropdownItemLayout}
          placeholder="Select language..."
        />
        {formik.errors.back && formik.touched.back && (
          <ValidationError error={formik.errors.back} />
        )}
      </div>
    </form>
  )
}

type GroupFormProps = {
  group?: GroupFormModel
  options?: string[]
  onSubmit: (group: GroupFormModel) => void
}

export type GroupFormModel = {
  id?: string
  name: string
  front: string
  back: string
}

const dropdownItemLayout = (option: string, props: any = null) => {
  return option ? (
    <div className="text-center text-accent-super-light font-extrabold text-xl">{option}</div>
  ) : (
    <span>{props.placeholder}</span>
  )
}

const ValidationError = ({ error }: { error: string | undefined }): ReactNode => {
  if (!error) {
    return null
  }
  console.log(error)

  return <div className="text-error ms-3">{error}</div>
}

export function validate(values: GroupFormModel): GroupFormModel {
  const errors = {} as GroupFormModel
  if (!values.name?.length) {
    errors.name = 'Type group name'
  }
  if (!values.front?.length) {
    errors.front = 'Choose or type front language'
  }
  if (!values.back?.length) {
    errors.back = 'Choose or type back language'
  }
  return errors
}
