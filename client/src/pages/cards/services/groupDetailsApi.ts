import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import { CardSummary } from "../models/cardSummary";
import { CardsSummaryResponse } from "../models/cardsSummaryResponse";
import { AddCardRequest } from "../models/requests/addCardRequest";
import AppendCardsRequest from "../models/requests/appendCardsRequest";
import { UpdateCardRequest } from "../models/requests/updateCardRequest";
import { GroupDetailsResponse } from "../models/groupDetailsSummary";
import { CardsOverview } from "../models/cardsOverview";
import { CardsSearchRequest } from "../models/requests/cardsSearchRequest";

export async function groupDetails(groupId: number): Promise<GroupDetailsResponse> {
  const response = await http.get<GroupDetailsResponse>(`/groups/details/${groupId}`);
  return response.data;
}

export async function cardsSummary(userId: string, groupId: number): Promise<CardsSummaryResponse> {
  const response = await http.get<CardsSummaryResponse>(`/cards/${userId}/${groupId}`);
  return response.data;
}

export async function updateCard(userId: string, groupId: number, card: CardSummary) {
  const request = {
    userId,
    groupId,
    cardId: card.id,
    front: {
      value: card.front.value,
      example: card.front.example,
      isUsed: card.front.isUsed,
      isTicked: card.front.isTicked,
    },
    back: {
      value: card.back.value,
      example: card.back.example,
      isUsed: card.back.isUsed,
      isTicked: card.back.isTicked,
    },
  } as UpdateCardRequest;
  try {
    const response = await http.put<{}>(`/cards/update`, request);
    return { data: response.data };
  } catch (error) {
    return { error };
  }
}

export async function addCard(userId: string, groupId: string, card: CardSummary) {
  const request = {
    userId,
    groupId,
    front: {
      value: card.front.value,
      example: card.front.example,
      isUsed: card.front.isUsed,
    },
    back: {
      value: card.back.value,
      example: card.back.example,
      isUsed: card.back.isUsed,
    },
  } as AddCardRequest;
  try {
    const response = await http.post<ApiResponse<string>>(`/cards/add`, request);
    return { data: response.data };
  } catch (error) {
    return { error };
  }
}

export async function deleteCard(userId: string, groupId: number, cardId: number) {
  const response = await http.delete<{}>(`/cards/delete/${userId}/${groupId}/${cardId}`);
  return { data: response.data };
}

export async function appendCards(
  userId: string,
  groupId: number,
  count: number,
  langauges: number
) {
  const request = {
    ownerId: userId,
    groupId,
    count,
    langauges,
  } as AppendCardsRequest;
  try {
    const response = await http.put<{}>(`/cards/append`, request);
    return { data: response.data };
  } catch (error) {
    return { error };
  }
}

export async function cardsOverview(ownerId: string): Promise<CardsOverview | any> {
  try {
    const response = await http.get<CardsOverview>(`/cards/overview/${ownerId}`);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function searchCards(request: CardsSearchRequest): Promise<CardSummary[] | any> {
  try {
    const response = await http.put<CardSummary[]>(`/cards/search`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function searchCardsCount(request: CardsSearchRequest): Promise<CardSummary[] | any> {
  try {
    const response = await http.get<CardSummary[]>(`/cards/search/count`, { params: request });
    return response.data;
  } catch (error) {
    return { error };
  }
}
