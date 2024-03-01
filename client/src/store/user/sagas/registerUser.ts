import { call, put, take } from '@redux-saga/core/effects'
import { PayloadAction } from '@reduxjs/toolkit'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { RegisterPayload } from '../action-payload'
import { login, setErrorMessage } from '../reducer'
import { RegisterRequest, RegisterResponse } from 'api/services/users'
import { AxiosError, AxiosResponse } from 'axios'

export function* registerUserEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<RegisterPayload> = yield take('user/register')

    const request: RegisterRequest = {
      userName: action.payload.userName,
      password: action.payload.password,
      passwordConfirmation: action.payload.passwordConfirmation,
      email: action.payload.email
    }

    const apiResponse: AxiosResponse<RegisterResponse> | AxiosError = yield call(
      api.register,
      request
    )

    let data: RegisterResponse
    if ('data' in apiResponse) {
      data = apiResponse.data as RegisterResponse
    } else {
      data = apiResponse.response?.data as RegisterResponse
    }

    switch (data.responseCode) {
      case api.RegisterResponseCode.Successful:
        yield put(login({ userName: action.payload.userName, password: action.payload.password }))
        break
      case api.RegisterResponseCode.UserNameAlreadyOccupied:
        yield put(setErrorMessage('User with the same name has already existed'))
        break
      case api.RegisterResponseCode.EmailAlreadyOccupied:
        yield put(setErrorMessage('User with the same email has already existed'))
        break
    }
  }
}
