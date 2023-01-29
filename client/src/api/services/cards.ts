import { ApiResponse } from "common/models/response";
import * as commands from "../commands";
import * as queries from "../queries";
import * as responses from "../responses";
import http from "./httpBase";
import { CardsOverview } from "pages/cardsSearch/models";
import { CardSummary } from "pages/cards/models";

export async function cardsSummary(
  userId: string,
  groupId: string
): Promise<responses.CardsSummaryResponse | boolean> {
  try {
    const resposnse = await http.get<responses.CardsSummaryResponse>(`/cards/${userId}/${groupId}`);
    return resposnse.data;
  } catch (error: any) {
    return false;
  }
}

export async function getCard(
  userId: string,
  groupId: string,
  cardId: string
): Promise<CardSummary | boolean> {
  try {
    const resposnse = await http.get<CardSummary>(`/cards/${userId}/${groupId}/${cardId}`);
    return resposnse.data;
  } catch (error: any) {
    return false;
  }
}

export async function updateCard(
  userId: string,
  groupId: string,
  card: CardSummary
): Promise<{} | boolean> {
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
  } as commands.UpdateCardRequest;
  try {
    const response = await http.put<{}>(`/cards/update`, request);
    return response.data;
  } catch (error) {
    return false;
  }
}

export async function addCard(
  userId: string,
  groupId: string,
  card: CardSummary
): Promise<string | boolean> {
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
  } as commands.AddCardRequest;
  try {
    const response = await http.post<string>(`/cards/add`, request);
    return (response.data as any).response;
  } catch (error) {
    return false;
  }
}

export async function deleteCard(
  userId: string,
  groupId: string,
  cardId: string
): Promise<{} | boolean> {
  try {
    await http.delete<{}>(`/cards/delete/${userId}/${groupId}/${cardId}`);
    return {};
  } catch (error: any) {
    return false;
  }
}

export async function appendCards(
  userId: string,
  groupId: string,
  count: number,
  langauges: number
) {
  const request = {
    ownerId: userId,
    groupId,
    count,
    langauges,
  } as commands.AppendCardsRequest;
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

export async function searchCards(request: queries.CardsSearchQuery): Promise<CardSummary[] | any> {
  try {
    const response = await http.put<CardSummary[]>(`/cards/search`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function searchCardsCount(request: queries.CardsSearchQuery): Promise<number | any> {
  try {
    const response = await http.put<number>(`/cards/search/count`, request);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function updateCard2(
  request: commands.UpdateCardRequest
): Promise<responses.UpdateCardResponse | boolean> {
  try {
    const response = await http.put<responses.UpdateCardResponse>(`/cards/update`, request);
    return response.data;
  } catch (error) {
    return false;
  }
}

export async function getCards(groupId: string): Promise<CardSummary[] | any> {
  try {
    const response = await http.get<CardSummary[]>(`/cards/${groupId}`);
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function tickCard(request: commands.TickCardRequest): Promise<ApiResponse<any>> {
  try {
    await http.put<any>("/cards/tick", request);
    return {
      isCorrect: true,
    } as ApiResponse<any>;
  } catch (e: any) {
    return e;
  }
}
