import { CardSummary } from 'pages/cards/models'

export interface GetCards {
  groupId: string
}

export interface GetCard {
  groupId: string
  cardId: string
}

export interface GetCardsSuccess {
  id: string
  name: string
  language1: string
  language2: string
  cards: CardSummary[]
}

export interface SelectCard {
  item: CardSummary
}

export interface UpdateCard {
  card: CardSummary
}

export interface UpdateCardSuccess {
  card: CardSummary
}

export interface AddCard {
  card: CardSummary
}

export interface AddCardSuccess {}

export interface DeleteCard {
  cardId: string
}

export interface DeleteCardSuccess {
  cardId: string
}

export interface SetFilterDrawer {
  drawer: number
}

export interface SetFilterLearning {
  isLearning: boolean
}

export interface SetFilterText {
  text: string
}

export interface SetFitlerIsTicked {
  isTicked: boolean
}

export interface SetFilteredCards {
  cards: CardSummary[]
}

export type UpdateGroupDetails = {
  name: string
  frontLanguage: string
  backLanguage: string
}
