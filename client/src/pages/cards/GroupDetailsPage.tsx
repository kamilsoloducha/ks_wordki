import './GroupDetailsPage.scss'
import * as selectors from 'store/cards/selectors'
import * as actions from 'store/cards/reducer'
import * as utils from './services'
import { ReactElement, useCallback, useEffect, useState } from 'react'
import CardsList from './components/CardsList'
import { shallowEqual, useDispatch, useSelector } from 'react-redux'
import { useParams } from 'react-router'
import InfoCard from './components/infoCard/InfoCard'
import Expandable from 'common/components/expandable/Expandable'
import { CardsFilter } from './models/cardsFilter'
import { Languages } from 'common/models/languages'
import LoadingSpinner from 'common/components/LoadingSpinner'
import { CardFormModel } from 'common/components/CardForm'
import CardDialog from 'common/components/CardDialog'
import { PageChangedEvent, Pagination } from 'common/components/Pagination'
import { CardSummary } from './models'
import { useTitle } from 'common/index'
import { GroupDetails } from './components/GroupDetails'
import { useEffectOnce } from 'common/hooks/useEffectOnce'
import { useLocalSettingsStorage } from 'common/hooks/useSettingsStorage'

export default function GroupDetailsPage(): ReactElement {
  const dispatch = useDispatch()
  const allCards = useSelector(selectors.selectCards)
  const filteredCardsFromStore = useSelector(selectors.selectFilteredCards)
  const filterState = useSelector(selectors.selectFilterState)
  const isLoading = useSelector(selectors.selectIsLoading)
  const groupDetails = useSelector(selectors.selectGroupDetails, shallowEqual)
  const [paginatedCards, setPaginatedCards] = useState<CardSummary[]>([])
  const [formItem, setFormItem] = useState<CardFormModel | undefined>(undefined)
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(
    useLocalSettingsStorage().get()?.paginationPageSize ?? 20
  )
  const { groupId } = useParams<{ groupId: string }>()

  useTitle(`Wordki - ${groupDetails.name}`)

  useEffectOnce(() => {
    dispatch(actions.getCards({ groupId: groupId ? groupId : '' }))
  }, [groupId, dispatch])

  useEffect(() => {
    const first = (page - 1) * pageSize
    const last = first + pageSize
    setPaginatedCards(filteredCardsFromStore.slice(first, last))
  }, [page, filteredCardsFromStore, pageSize])

  const onItemSelected = (item: CardSummary) => {
    dispatch(actions.selectCard({ item }))
    setFormItem(getFormModelFromCardSummary(item))
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
    if (udpdatedCard.id !== '') {
      dispatch(actions.updateCard({ card: udpdatedCard }))
      setFormItem(undefined)
    } else {
      dispatch(actions.addCard({ card: udpdatedCard }))
      onAddCard()
    }
  }

  const onDelete = (item: CardFormModel) => {
    if (!item.cardId) {
      return
    }
    dispatch(actions.deleteCard({ cardId: item.cardId }))
    setFormItem(undefined)
  }

  const onCancel = () => {
    dispatch(actions.resetSelectedCard())
    setFormItem(undefined)
  }

  const onClickSetFilter = (filter: CardsFilter) => {
    if (filter === 1) {
      dispatch(actions.resetFilters())
    } else if (filter === 2) {
      dispatch(actions.setFilterLearning({ isLearning: true }))
    } else if (filter === 3) {
      dispatch(actions.setFilterLearning({ isLearning: false }))
    } else if (filter >= 4 && filter <= 8) {
      dispatch(actions.setFilterDrawer({ drawer: filter - 3 }))
    } else if (filter === 9) {
      dispatch(actions.setFilterIsTicked({ isTicked: true }))
    }
  }

  const onAddCard = () => {
    const cardTemplate = {
      id: '',
      front: { value: '', example: '', isUsed: false },
      back: { value: '', example: '', isUsed: false }
    } as CardSummary
    dispatch(actions.selectCard({ item: cardTemplate }))
    setFormItem(getFormModelFromCardSummary(cardTemplate))
  }

  const onPageChagned = (event: PageChangedEvent) => {
    setPage(event.currectPage)
  }

  const onSearchChanged = useCallback(
    (text: string) => {
      dispatch(actions.setFilterText({ text }))
    },
    [dispatch]
  )

  if (isLoading) {
    return <LoadingSpinner />
  }

  return (
    <div className="group-detail-main-container">
      <GroupDetails />
      <div className="group-details-cards-info-container">
        <div className="group-details-info-card">
          <InfoCard
            label="all cards"
            value={allCards.length}
            classNameOverriden="info-card-blue"
            onClick={() => onClickSetFilter(CardsFilter.All)}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="learning"
            value={utils.getLearningCardCount(allCards)}
            classNameOverriden="info-card-green"
            onClick={() => onClickSetFilter(CardsFilter.Learning)}
          />
        </div>
        {drawers.map((item) => (
          <div className="group-details-info-card" key={item}>
            <InfoCard
              label={'drawer ' + item}
              value={utils.getCardsCountFromDrawerCount(allCards, item)}
              classNameOverriden={'info-card-drawer-' + item}
              onClick={() => onClickSetFilter(3 + item)}
            />
          </div>
        ))}
      </div>
      <Expandable>
        <div className="group-details-info-card">
          <InfoCard
            label="waiting"
            value={2 * allCards.length - utils.getLearningCardCount(allCards)}
            classNameOverriden="info-card-gray"
            onClick={() => onClickSetFilter(CardsFilter.Waiting)}
          />
        </div>
        <div className="group-details-info-card">
          <InfoCard
            label="ticked"
            value={utils.getTickedCardCount(allCards)}
            classNameOverriden="info-card-gray"
            onClick={() => onClickSetFilter(CardsFilter.Ticked)}
          />
        </div>
      </Expandable>
      <div className="group-details-paginator">
        <Pagination
          page={page}
          pageSize={pageSize}
          totalCount={filteredCardsFromStore.length}
          onPageChagned={onPageChagned}
          search={filterState.text}
          onSearchChanged={onSearchChanged}
          onPageSizeChanged={setPageSize}
        />
      </div>
      <div className="group-details-card-list-container">
        <CardsList cards={paginatedCards} onItemSelected={onItemSelected} />
      </div>
      <CardDialog
        card={formItem}
        onHide={onCancel}
        onSubmit={onFormSubmit}
        onDelete={onDelete}
        frontLanguage={Languages[parseInt(groupDetails.language1)]}
        backLanguage={Languages[parseInt(groupDetails.language2)]}
      />
    </div>
  )
}

function getFormModelFromCardSummary(card: CardSummary): CardFormModel {
  return {
    cardId: card.id,
    frontValue: card.front.value,
    frontExample: card.front.example,
    frontEnabled: card.front.isUsed,
    backValue: card.back.value,
    backExample: card.back.example,
    backEnabled: card.back.isUsed,
    isTicked: card.front.isTicked
  } as CardFormModel
}

const drawers = [1, 2, 3, 4, 5]
