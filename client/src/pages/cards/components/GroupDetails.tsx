import { selectGroupDetails } from 'store/cards/selectors'
import { shallowEqual, useSelector } from 'react-redux'
import { Languages, getLanguageByType } from 'common/index'
import GroupDialog from 'common/components/GroupDialog'
import { useState } from 'react'
import { GroupFormModel } from 'common/components/GroupForm'
import { selectLanguages } from 'store/lesson/selectors'
import { useAppDispatch } from 'store/store'
import { addCard, updateGroupDetails } from 'store/cards/reducer'
import CardDialog from 'common/components/CardDialog'
import { CardFormModel } from 'common/components/CardForm'
import { CardSummary } from 'pages/cards/models'

export function GroupDetails() {
  const dispatch = useAppDispatch()
  const { id, name, language1, language2 } = useSelector(selectGroupDetails, shallowEqual)
  const cardSides = useSelector(selectLanguages) // todo: move it  to group Details directly

  const [editedGroup, setEditedGroup] = useState<GroupFormModel | undefined>(undefined)
  const [newCardItem, setNewCardItem] = useState<CardFormModel | undefined>(undefined)

  const onEditGroupClicked = () => {
    const group: GroupFormModel = {
      id: id,
      name: name,
      front: language1,
      back: language2
    }
    setEditedGroup(group)
  }

  const onAddCardClicked = () => {
    const card: CardFormModel = {
      frontValue: '',
      frontExample: '',
      frontEnabled: false,
      backValue: '',
      backExample: '',
      backEnabled: false,
      comment: '',
      isTicked: false
    }
    setNewCardItem(card)
  }

  const onFormSubmit = (item: CardFormModel): void => {
    const udpdatedCard = {
      id: item.cardId,
      front: {
        value: item.frontValue,
        example: item.frontExample,
        isUsed: item.frontEnabled,
        isTicked: item.isTicked
      },
      back: {
        value: item.backValue,
        example: item.backExample,
        isUsed: item.backEnabled,
        isTicked: item.isTicked
      }
    } as CardSummary
    dispatch(addCard({ card: udpdatedCard }))
    onAddCardClicked()
  }

  const onSubmitGroupDialog = (group: GroupFormModel) => {
    if (!group) {
      return
    }
    dispatch(
      updateGroupDetails({ name: group.name, frontLanguage: group.front, backLanguage: group.back })
    )
    setEditedGroup(undefined)
  }

  return (
    <>
      <div className="group my-3 flex w-full items-center justify-between rounded-lg bg-accent-light p-3 text-accent-super-light">
        <div className="m-3 flex items-center align-middle text-4xl font-extrabold">
          <div className="flex flex-col">
            <img className="" src={getLanguageByType(parseInt(language1)).icon} width="32px" />
            <img className="" src={getLanguageByType(parseInt(language2)).icon} width="32px" />
          </div>
          <p className="ms-2">{name}</p>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="ms-2 hidden size-10 cursor-pointer rounded-md p-2 hover:bg-lighter-a-bit group-hover:block"
            onClick={onEditGroupClicked}
          >
            <path d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L6.832 19.82a4.5 4.5 0 0 1-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 0 1 1.13-1.897L16.863 4.487Zm0 0L19.5 7.125" />
          </svg>
        </div>
        <button
          className="rounded-md px-2 py-4 font-bold text-accent-super-light hover:bg-lighter-a-bit"
          onClick={onAddCardClicked}
        >
          + Add Card
        </button>
      </div>
      <GroupDialog
        cardSides={cardSides}
        group={editedGroup}
        onHide={() => setEditedGroup(undefined)}
        onSubmit={onSubmitGroupDialog}
      />
      <CardDialog
        card={newCardItem}
        onHide={() => setNewCardItem(undefined)}
        onSubmit={onFormSubmit}
        frontLanguage={Languages[parseInt(language1)]}
        backLanguage={Languages[parseInt(language2)]}
      />
    </>
  )
}
