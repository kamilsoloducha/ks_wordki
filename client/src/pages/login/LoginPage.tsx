import { Toast } from "primereact/toast";
import { useFormik } from "formik";
import { ReactElement, useRef } from "react";
import { Redirect } from "react-router";
import LoginRequest from "./models/loginRequest";
import { useDispatch, useSelector } from "react-redux";
import { getLoginUser } from "store/user/actions";
import { selectUserId } from "store/user/selectors";

function LoginPage(): ReactElement {
  const toast = useRef<Toast>(null);
  const userId = useSelector(selectUserId);
  const dispatch = useDispatch();

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => onSubmit(values),
    validate,
  });

  if (userId) {
    return <Redirect to="/dashboard" />;
  }

  const onSubmit = async (values: FormModel) => {
    const request: LoginRequest = { ...values };
    dispatch(getLoginUser(request.userName, request.password));
  };

  return (
    <>
      <h1>Logowanie</h1>
      <form onSubmit={formik.handleSubmit} autoComplete="off">
        <div>
          <label htmlFor="userName">Nazwa użytkownika</label>
          <input
            id="userName"
            name="userName"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.userName}
            autoComplete="off"
          />
          {formik.errors.userName && formik.touched.userName ? (
            <div>{formik.errors.userName}</div>
          ) : null}
        </div>
        <div>
          <label htmlFor="password">Hasło</label>
          <input
            id="password"
            name="password"
            type="password"
            onChange={formik.handleChange}
            value={formik.values.password}
            autoComplete="off"
          />
          {formik.errors.password && formik.touched.password ? (
            <div>{formik.errors.password}</div>
          ) : null}
        </div>
        <input type="submit" value="Zaloguj" />
      </form>
      <Toast ref={toast} />
    </>
  );
}

export default LoginPage;

interface FormModel {
  userName: string;
  password: string;
}

const initialValues: FormModel = {
  userName: "user_name",
  password: "pass",
};

const validate = (values: FormModel): FormModel => {
  const errors = {} as FormModel;
  if (!values.userName?.length) {
    errors.userName = "Pole jest wymagane";
  }
  if (!values.password?.length) {
    errors.password = "Pole jest wymagane";
  }
  return errors;
};
