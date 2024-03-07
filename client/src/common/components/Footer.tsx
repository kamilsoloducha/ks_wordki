import { ReactElement } from 'react'

type FooterProps = {
  formId?: string
  onCancelClicked?: () => void
  onDeleteClicked?: () => void
}

export default function Footer({
  formId,
  onCancelClicked,
  onDeleteClicked
}: FooterProps): ReactElement {
  return (
    <div className="flex place-content-between rounded-b-md bg-main">
      <div>
        {onDeleteClicked && (
          <button
            className="m-3 cursor-pointer rounded-md border-0 bg-red-700 px-10 py-2 text-stone-200 hover:bg-red-800"
            onClick={onDeleteClicked}
          >
            Delete
          </button>
        )}
      </div>
      <div>
        <button
          className=" m-3 cursor-pointer rounded-md border-0 bg-zinc-700 px-10 py-2 text-stone-200 hover:bg-zinc-800"
          onClick={onCancelClicked}
        >
          Cancel
        </button>
        <button
          type="submit"
          form={formId}
          className=" m-3 cursor-pointer rounded-md border-0 bg-green-700 px-10 py-2 text-stone-200 hover:bg-green-800"
        >
          Save
        </button>
      </div>
    </div>
  )
}
