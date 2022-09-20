import "./RegisterPage.scss";
import { useFormik } from "formik";
import { ReactElement, useEffect } from "react";
import * as selectors from "store/user/selectors";
import { initialValue, RegisterFormModel } from "./models";
import { validate } from "./services/registerFormValidator";
import { useTitle } from "common";
import { useAppDispatch, useAppSelector } from "store/store";
import { register, setErrorMessage } from "store/user/reducer";
import { useNavigate } from "react-router-dom";

export default function RegisterPage(): ReactElement {
  useTitle("Wordki - Register");
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const userId = useAppSelector(selectors.selectUserId);
  const isLoading = useAppSelector(selectors.selectIsLoading);
  const errorMessage = useAppSelector(selectors.selectErrorMessage);

  useEffect(() => {
    dispatch(setErrorMessage(""));
  }, [dispatch]);

  const formik = useFormik({
    initialValues: initialValue,
    onSubmit: (values) => onSubmit(values),
    validate,
  });

  const onSubmit = async (model: RegisterFormModel) => {
    dispatch(
      register({
        userName: model.userName,
        email: model.email,
        password: model.password,
        passwordConfirmation: model.passwordConfirmation,
      })
    );
  };

  if (userId) {
    navigate('/dashboard');
  }
  
  return (
    <div className="register-page-container">
      <form className="register-form" onSubmit={formik.handleSubmit} autoComplete="off">
        <div className="register-form-header">Register</div>
        <div className="register-input-item">
          <input
            id="userName"
            name="userName"
            value={formik.values.userName}
            onChange={formik.handleChange}
            type="text"
            autoComplete="off"
            placeholder="User name"
            disabled={isLoading}
          />
          {formik.errors.userName && formik.touched.userName ? (
            <div className="error-message">{formik.errors.userName}</div>
          ) : null}
        </div>

        <div className="register-input-item">
          <input
            id="email"
            name="email"
            value={formik.values.email}
            onChange={formik.handleChange}
            type="email"
            autoComplete="off"
            placeholder="E-mail"
            disabled={isLoading}
          />
          {formik.errors.email && formik.touched.email ? (
            <div className="error-message">{formik.errors.email}</div>
          ) : null}
        </div>

        <div className="register-input-item">
          <input
            id="password"
            name="password"
            value={formik.values.password}
            onChange={formik.handleChange}
            type="password"
            autoComplete="off"
            placeholder="Password"
            disabled={isLoading}
          />
          {formik.errors.password && formik.touched.password ? (
            <div className="error-message">{formik.errors.password}</div>
          ) : null}
        </div>

        <div className="register-input-item">
          <input
            id="passwordConfirmation"
            name="passwordConfirmation"
            value={formik.values.passwordConfirmation}
            onChange={formik.handleChange}
            type="password"
            autoComplete="off"
            placeholder="Password Confirmation"
            disabled={isLoading}
          />
          {formik.errors.passwordConfirmation && formik.touched.passwordConfirmation ? (
            <div className="error-message">{formik.errors.passwordConfirmation}</div>
          ) : null}
        </div>
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        <input type="submit" value="Create" disabled={isLoading} />
      </form>
    </div>
  );
}
