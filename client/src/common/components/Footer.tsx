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
    <div className="bg-main flex place-content-between rounded-b-md">
      <div>
        {onDeleteClicked && (
          <button
            className="bg-red-700 hover:bg-red-800 text-stone-200 cursor-pointer py-2 px-10 border-0 rounded-md m-3"
            onClick={onDeleteClicked}
          >
            Delete
          </button>
        )}
      </div>
      <div>
        <button
          className=" bg-zinc-700 hover:bg-zinc-800 text-stone-200 cursor-pointer py-2 px-10 border-0 rounded-md m-3"
          onClick={onCancelClicked}
        >
          Cancel
        </button>
        <button
          type="submit"
          form={formId}
          className=" bg-green-700 hover:bg-green-800 text-stone-200 cursor-pointer py-2 px-10 border-0 rounded-md m-3"
        >
          Save
        </button>
      </div>
    </div>
  )
}
