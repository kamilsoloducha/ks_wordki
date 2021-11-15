import { ApiResponse } from "common/models/response";
import http from "common/services/http/http";
import { CardSummary } from "../models/cardSummary";
import { GroupDetailsResponse } from "../models/groupDetailsResponse";
import { AddCardRequest } from "../models/requests/addCardRequest";
import { UpdateCardRequest } from "../models/requests/updateCardRequest";

export async function groupDetails(userId: string, groupId: string) {
  const response = await http.get<GroupDetailsResponse>(
    `/groups/${userId}/${groupId}`
  );
  return { data: response.data };
}

export async function updateCard(
  userId: string,
  groupId: string,
  card: CardSummary
) {
  const request = {
    userId,
    groupId,
    cardId: card.id,
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
    comment: card.comment,
  } as UpdateCardRequest;
  try {
    const response = await http.put<{}>(`/cards/update`, request);
    return { data: response.data };
  } catch (error) {
    return { error };
  }
}

export async function addCard(
  userId: string,
  groupId: string,
  card: CardSummary
) {
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
    comment: card.comment,
  } as AddCardRequest;
  try {
    const response = await http.post<ApiResponse<string>>(
      `/cards/add`,
      request
    );
    return { data: response.data };
  } catch (error) {
    return { error };
  }
}

export async function deleteCard(
  userId: string,
  groupId: string,
  cardId: string
) {
  const response = await http.delete<{}>(
    `/cards/delete/${userId}/${groupId}/${cardId}`
  );
  return { data: response.data };
}

export function addGroup(userId: string, card: any): any {
  throw new Error("Function not implemented.");
}
