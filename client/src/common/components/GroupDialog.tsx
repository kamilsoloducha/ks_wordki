import { Language } from 'pages/lessonSettings/models/languages'
import { ReactElement } from 'react'
import Footer from './Footer'
import { GroupForm, GroupFormModel } from './GroupForm'
import { Modal } from 'common/components/Modal'

export default function GroupDialog({
  cardSides,
  group,
  onHide,
  onSubmit,
  onDelete
}: GroupDialogProps): ReactElement {
  const ondelete: (() => void) | undefined = onDelete
    ? () => {
        if (onDelete && group?.id) onDelete(group.id)
      }
    : undefined

  const footer = <Footer onCancelClicked={onHide} onDeleteClicked={ondelete} formId="group-form" />

  const header = (
    <div className="rounded-t-md bg-main">
      <p className="p-5 text-xl font-extrabold">{group?.id ? 'Editing Group' : 'Creating Group'}</p>
    </div>
  )

  return (
    <Modal isOpen={!!group} onClose={onHide} footer={footer} header={header}>
      <div className="model w-[96vw] lg:w-[50vw]">
        <GroupForm options={cardSides.map((x) => x.language)} group={group} onSubmit={onSubmit} />
      </div>
    </Modal>
  )
}

type GroupDialogProps = {
  group?: GroupFormModel
  cardSides: Language[]
  onHide: () => void
  onSubmit: (item: GroupFormModel) => void
  onDelete?: (groupId: string) => void
}
