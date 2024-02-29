import { ReactNode, useRef } from 'react'
import http from 'api/services/httpBase'
import { useEffectOnce, useUserStorage } from 'common/hooks'
import { ConfirmationModal, ConfirmationModalRef } from 'common/components/ConfirmationModal'

export default function Axios({ children }: AxiosProps) {
  const { get } = useUserStorage()
  const confirmationModel = useRef<ConfirmationModalRef>(null)

  useEffectOnce(() => {
    http.interceptors.request.use(
      (req) => {
        const user = get()
        if (user) {
          req.headers.Authorization = 'Bearer ' + user.token
        }
        return req
      },
      (error) => {
        console.error(error)
      }
    )

    http.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error?.response?.status === 401) {
          console.error('Response code: 401')
          window.location.href = '/logout'
          return
        }
        if (error?.response?.status >= 500) {
          console.error('Response code: 500', error)
          if (confirmationModel.current) {
            confirmationModel.current.show(error.message, 'Error')
          }
        }
        console.error(error)
        return error
      }
    )
  })
  return (
    <>
      <ConfirmationModal ref={confirmationModel} />
      {children as ReactNode}
    </>
  )
}

type AxiosProps = {
  children?: ReactNode
}
