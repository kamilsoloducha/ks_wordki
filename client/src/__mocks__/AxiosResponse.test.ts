import { AxiosResponse } from 'axios'

export function mockAxiosResponse<TResponse>(
  response: TResponse
): Promise<AxiosResponse<TResponse>> {
  return Promise.resolve({
    data: response,
    status: 200
  } as AxiosResponse<TResponse>)
}
