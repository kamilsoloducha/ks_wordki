import "./LoginPage.scss";
import { useFormik } from "formik";
import { ReactElement, useEffect } from "react";
import { Redirect } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { login, setErrorMessage } from "store/user/actions";
import * as selectors from "store/user/selectors";
import { initialValues, LoginFormModel } from "./models";
import { validate } from "./services/loginFormValidator";
import { useTitle } from "common";

export default function LoginPage(): ReactElement {
  useTitle("Wordki - Login");
  const userId = useSelector(selectors.selectUserId);
  const isLoading = useSelector(selectors.selectIsLoading);
  const errorMessage = useSelector(selectors.selectErrorMessage);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(setErrorMessage(""));
  }, [dispatch]);

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => onSubmit(values),
    validate,
  });

  if (userId) {
    return <Redirect to="/dashboard" />;
  }

  const onSubmit = (values: LoginFormModel) => {
    dispatch(login(values.userName, values.password));
  };

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
  );
}
