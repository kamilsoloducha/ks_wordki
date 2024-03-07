export interface LoginPayload {
  userName: string
  password: string
}

export interface LoginSuccessPayload {
  id: string
  token: string
  expirationDate: string
}

export interface RegisterPayload {
  userName: string
  email: string
  password: string
  passwordConfirmation: string
}
