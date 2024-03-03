import { AxiosError, AxiosResponse } from 'axios'
import http from './httpBase'

const USERS_PATH = '/users'

// ------------------------
// LOGIN
// ------------------------
export function login(request: LoginRequest): Promise<AxiosResponse<LoginResponse> | AxiosError> {
  return http.put(`${USERS_PATH}` + '/login', request)
}

export type LoginRequest = {
  userName: string
  password: string
}

export type LoginResponse = {
  responseCode: number
  id: string
  userName: string
  token: string
  expirationDateTime: string
}

// ------------------------
// REGISTER
// ------------------------

export function register(
  request: LoginRequest
): Promise<AxiosResponse<LoginResponse> | AxiosError> {
  return http.post(`${USERS_PATH}` + '/register', request)
}

export type RegisterRequest = {
  userName: string
  password: string
  passwordConfirmation: string
  email: string
}

export type RegisterResponse = {
  responseCode: number
  id: string
  userName: string
  token: string
  expirationDateTime: string
}
