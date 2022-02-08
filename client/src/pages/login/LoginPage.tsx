import "./LoginPage.scss";
import { useFormik } from "formik";
import { ReactElement, useEffect } from "react";
import { Redirect } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { getLoginUser } from "store/user/actions";
import { selectIsLoading, selectUserId } from "store/user/selectors";

export default function LoginPage(): ReactElement {
  const userId = useSelector(selectUserId);
  const isLoading = useSelector(selectIsLoading);

  const dispatch = useDispatch();
  useEffect(() => {
    document.title = "Wordki - Login";
  }, []);

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => onSubmit(values),
    validate,
  });

  if (userId) {
    return <Redirect to="/dashboard" />;
  }

  const onSubmit = (values: FormModel) => {
    dispatch(getLoginUser(values.userName, values.password));
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
        <input type="submit" value="Login" disabled={isLoading} />
      </form>
    </div>
  );
}

interface FormModel {
  userName: string;
  password: string;
}

const initialValues: FormModel = {
  userName: "",
  password: "",
};

const validate = (values: FormModel): FormModel => {
  const errors = {} as FormModel;
  if (!values.userName?.length) {
    errors.userName = "Field is required";
  }
  if (!values.password?.length) {
    errors.password = "Field is required";
  }
  return errors;
};
