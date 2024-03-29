import { useUserStorage } from 'common/index'
import { ReactElement, useEffect } from 'react'
import { Navigate } from 'react-router-dom'
import { useAppDispatch } from 'store/store'
import { logout } from 'store/user/reducer'

export default function LogoutPage(): ReactElement {
  const { remove } = useUserStorage()
  remove()
  const dispatch = useAppDispatch()
  useEffect(() => {
    localStorage.removeItem('id')
    localStorage.removeItem('token')
    dispatch(logout())
  }, [dispatch])
  return <Navigate to={'/login'} />
}
