import * as selectors from 'store/cardsSearch/selectors'
import * as actions from 'store/cardsSearch/reducer'
import { Fragment, ReactElement, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { PageChangedEvent, Pagination } from 'common/components/Pagination'
import LoadingSpinner from 'common/components/LoadingSpinner'
import CardDialog from 'common/components/CardDialog'
import { CardFormModel } from 'common/components/CardForm'
import { CardsOverview, CardSummary } from './models'
import { Row } from './components/row/Row'
import { useTitle } from 'common/index'
import { useEffectOnce } from 'common/hooks/useEffectOnce'
import { TriStateCheckbox } from 'primereact/tristatecheckbox'

export default function CardsPage(): ReactElement {
  useTitle('Wordki - Cards')
  const dispatch = useDispatch()
  const cards = useSelector(selectors.selectCards)
  const cardsCount = useSelector(selectors.selectCardsCount)
  const filter = useSelector(selectors.selectFilter)
  const isSearching = useSelector(selectors.selectIsSearching)
  const overview = useSelector(selectors.selectOverview)

  const [selectedItem, setSelectedItem] = useState<CardSummary | undefined>(undefined)

  useEffectOnce(() => {
    dispatch(actions.getOverview())
    dispatch(actions.search())
  })

  const onPageChagned = (event: PageChangedEvent) => {
    dispatch(actions.filterSetPagination({ pageNumber: event.currectPage, pageSize: event.count }))
  }

  const onPaginationPageSizeChanged = (newSize: number) => {
    dispatch(actions.filterSetPagination({ pageNumber: filter.pageNumber, pageSize: newSize }))
  }

  const onSearchChanged = (searchingTerm: string) => {
    dispatch(actions.filterSetTerm({ searchingTerm }))
  }

  const setPageSize = (size: number) => {
    dispatch(actions.filterSetPagination({ pageNumber: filter.pageNumber, pageSize: size }))
  }

  const clearFilters = () => {
    dispatch(actions.filterReset())
  }

  const tickedOnly = (e: any) => {
    dispatch(actions.filterSetTickedOnly({ tickedOnly: e.value }))
  }

  const lessonIncludedOnly = (e: any) => {
    dispatch(actions.filterSetLessonIncluded({ lessonIncluded: e.value }))
  }

  const onItemSelected = (card: CardSummary) => {
    setSelectedItem(card)
  }

  const onDelete = (model: CardFormModel | null) => {
    if (!selectedItem || !model) {
      return
    }
    dispatch(actions.deleteCard({ cardId: selectedItem.id, groupId: selectedItem.groupId }))
    setSelectedItem(undefined)
  }

  const onSubmit = (model: CardFormModel | null) => {
    if (!selectedItem || !model) {
      return
    }
    const card: CardSummary = {
      ...selectedItem,
      front: {
        ...selectedItem.front,
        value: model.frontValue,
        example: model.frontExample,
        isTicked: model.isTicked,
        isUsed: model.frontEnabled
      },
      back: {
        ...selectedItem.back,
        value: model.backValue,
        example: model.backExample,
        isTicked: model.isTicked,
        isUsed: model.backEnabled
      }
    }
    dispatch(actions.updateCard({ card }))
    setSelectedItem(undefined)
  }

  const onCancel = () => {
    setSelectedItem(undefined)
  }

  return (
    <div>
      {isSearching && <LoadingSpinner />}
      <Overview overview={overview} />
      <button onClick={() => setPageSize(20)}>20</button>
      <button onClick={() => setPageSize(50)}>50</button>
      <button onClick={() => setPageSize(100)}>100</button>
      <button onClick={clearFilters}>All</button>
      <TriStateCheckbox value={filter.tickedOnly} onChange={tickedOnly} />
      <label>Ticked Only</label>
      <TriStateCheckbox value={filter.lessonIncluded} onChange={lessonIncludedOnly} />
      <label>Lesson included</label>
      <Pagination
        totalCount={cardsCount}
        pageSize={filter.pageSize}
        onPageChagned={onPageChagned}
        search={filter.searchingTerm}
        onSearchChanged={onSearchChanged}
        onPageSizeChanged={onPaginationPageSizeChanged}
      />
      {cards.map((item: any, index: number) => (
        <Fragment key={index}>
          <Row card={item} onClick={onItemSelected} />
        </Fragment>
      ))}
      <CardDialog
        card={getFormModelFromCardSummary(selectedItem)}
        onHide={onCancel}
        onSubmit={onSubmit}
        onDelete={onDelete}
      />
    </div>
  )
}

function Overview({ overview }: OverviewModel): ReactElement {
  return (
    <div>
      totalCount: <b>{overview.all}</b>
    </div>
  )
}

interface OverviewModel {
  overview: CardsOverview
}

function getFormModelFromCardSummary(card: CardSummary | undefined): CardFormModel | undefined {
  if (!card) {
    return undefined
  }
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
