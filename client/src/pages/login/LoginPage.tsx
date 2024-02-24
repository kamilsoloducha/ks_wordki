import './LoginPage.scss'
import { useFormik } from 'formik'
import { ReactElement, useEffect } from 'react'
import * as selectors from 'store/user/selectors'
import { initialValues, LoginFormModel } from './models'
import { validate } from './services/loginFormValidator'
import { useTitle } from 'common/index'
import { useAppDispatch, useAppSelector } from 'store/store'
import { login, setErrorMessage } from 'store/user/reducer'
import { useNavigate } from 'react-router-dom'

export default function LoginPage(): ReactElement {
  useTitle('Wordki - Login')
  const userId = useAppSelector(selectors.selectUserId)
  const isLoading = useAppSelector(selectors.selectIsLoading)
  const errorMessage = useAppSelector(selectors.selectErrorMessage)
  const navigate = useNavigate()

  const dispatch = useAppDispatch()

  useEffect(() => {
    dispatch(setErrorMessage(''))
  }, [dispatch])

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => onSubmit(values),
    validate
  })

  if (userId) {
    throw new Error('it should not happen!!')
    navigate('/dashboard')
  }

  const onSubmit = (values: LoginFormModel) => {
    dispatch(login({ userName: values.userName, password: values.password }))
  }

  return (
    <div className="login-page-container">
      <form className="login-form" onSubmit={formik.handleSubmit} autoComplete="off">
        <div className="login-form-header">Login</div>

        <div className="login-input-item">
          <input
            id="userName"
            name="userName"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.userName}
            autoComplete="off"
            placeholder="User Name"
            disabled={isLoading}
          />
          {formik.errors.userName && formik.touched.userName ? (
            <div className="error-message">{formik.errors.userName}</div>
          ) : null}
        </div>
        <div className="login-input-item">
          <input
            id="password"
            name="password"
            type="password"
            onChange={formik.handleChange}
            value={formik.values.password}
            autoComplete="off"
            placeholder="Password"
            disabled={isLoading}
          />
          {formik.errors.password && formik.touched.password ? (
            <div className="error-message">{formik.errors.password}</div>
          ) : null}
        </div>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        <input type="submit" value="Login" disabled={isLoading} />
      </form>
    </div>
  )
}
