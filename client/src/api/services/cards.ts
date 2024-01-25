import { ApiResponse } from "common/models/response";
import * as commands from "../commands";
import * as queries from "../queries";
import http from "./httpBase";
import { CardsOverview } from "pages/cardsSearch/models";
import { CardSummary } from "pages/cards/models";

export async function cardsSummary(groupId: string): Promise<CardSummary[] | boolean> {
  try {
    const resposnse = await http.get<CardSummary[]>(`/cards/summaries/${groupId}`);
    return resposnse.data;
  } catch (error: any) {
    return false;
  }
}

export async function getCard(cardId: string): Promise<CardSummary | boolean> {
  try {
    const resposnse = await http.get<CardSummary>(`/cards/summary/${cardId}`);
    return resposnse.data;
  } catch (error: any) {
    return false;
  }
}

export async function updateCard(card: CardSummary): Promise<{} | boolean> {
  const request = {
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
    comment: "",
  } as commands.UpdateCardRequest;
  try {
    const response = await http.put<{}>(`/cards/update/${card.id}`, request);
    return response.data;
  } catch (error) {
    return false;
  }
}

export async function addCard(groupId: string, card: CardSummary): Promise<string | boolean> {
  const request = {
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
    comment: "",
  } as commands.AddCardRequest;
  try {
    const response = await http.post<string>(`/cards/add/${groupId}`, request);
    return response.data;
  } catch (error) {
    return false;
  }
}

export async function addCardRequest(
  groupId: string,
  request: commands.AddCardRequest
): Promise<string | boolean> {
  try {
    const response = await http.post<string>(`/cards/add/${groupId}`, request);
    return response.data;
  } catch (error) {
    return false;
  }
}

export async function deleteCard(cardId: string): Promise<{} | boolean> {
  try {
    await http.delete<{}>(`/cards/${cardId}`);
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
    const response = await http.get<CardSummary[]>(`/cards/search`, { params: request });
    return response.data;
  } catch (error) {
    return { error };
  }
}

export async function searchCardsCount(request: queries.CardsSearchQuery): Promise<number | any> {
  try {
    const response = await http.get<number>(`/cards/search/count`, { params: request });
    return response.data;
  } catch (error) {
    return { error };
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

export async function tickCard(cardId: string): Promise<ApiResponse<any>> {
  try {
    await http.put<any>(`/cards/tick/${cardId}`);
    return {
      isCorrect: true,
    } as ApiResponse<any>;
  } catch (e: any) {
    return e;
  }
}
