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
      <div className="bg-accent-light text-accent-super-light my-3 p-3 w-full flex justify-between items-center rounded-lg group">
        <div className="m-3 text-4xl flex align-middle items-center font-extrabold">
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
            className="w-10 h-10 ms-2 p-2 hidden group-hover:block cursor-pointer hover:bg-lighter-a-bit rounded-md"
            onClick={onEditGroupClicked}
          >
            <path d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L6.832 19.82a4.5 4.5 0 0 1-1.897 1.13l-2.685.8.8-2.685a4.5 4.5 0 0 1 1.13-1.897L16.863 4.487Zm0 0L19.5 7.125" />
          </svg>
        </div>
        <button
          className="text-accent-super-light hover:bg-lighter-a-bit rounded-md px-2 py-4 font-bold"
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
