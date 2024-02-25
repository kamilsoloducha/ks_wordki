import { ReactElement } from 'react'
import { Navigate } from 'react-router-dom'
import { useUserStorage } from 'hooks/useUserStorage'

function ProtectedRoute({
  children,
  isLoginForbiden,
  isLoginRequired
}: ProtectedRouteProps): ReactElement {
  const { get } = useUserStorage()
  const userSession = get()

  if (isLoginForbiden && isLoginRequired) {
    throw new Error('Wrong params')
  }

  if (isLoginRequired) {
    return userSession?.id ? children : <Navigate to={'/login'} />
  }
  if (isLoginForbiden) {
    return userSession?.id ? <Navigate to={'/'} /> : children
  }

  return children
}

type ProtectedRouteProps = {
  children: ReactElement
  isLoginRequired: boolean
  isLoginForbiden: boolean
}

export default ProtectedRoute
