import { call, put, take } from '@redux-saga/core/effects'
import { PayloadAction } from '@reduxjs/toolkit'
import * as api from 'api/index'
import { SagaIterator } from 'redux-saga'
import { LoginPayload } from '../action-payload'
import { loginSuccess, setErrorMessage } from '../reducer'
import { useUserStorage } from 'common/index'
import { LoginRequest, LoginResponse } from 'api/services/users'
import { AxiosError, AxiosResponse } from 'axios'

export function* loginUserEffect(): SagaIterator {
  while (true) {
    const action: PayloadAction<LoginPayload> = yield take('user/login')
    const request = {
      userName: action.payload.userName,
      password: action.payload.password
    } as LoginRequest
    const apiResponse: AxiosResponse<LoginResponse> | AxiosError = yield call(api.login, request)

    let data: LoginResponse
    if ('data' in apiResponse) {
      data = apiResponse.data as LoginResponse
    } else {
      data = apiResponse.response?.data as LoginResponse
    }

    switch (data.responseCode) {
      case api.LoginResponseCode.Successful:
        const { set } = useUserStorage()
        set({
          id: data.id,
          name: data.id,
          token: data.token,
          expirationDate: new Date(data.expirationDateTime)
        })
        yield put(
          loginSuccess({
            token: data.token,
            id: data.id,
            expirationDate: data.expirationDateTime
          })
        )
        break
      case api.LoginResponseCode.UserNotFound:
        yield put(setErrorMessage('Incorrect username or password.'))
        break
    }
  }
}
