import { useFormik } from "formik";
import { Toast } from "primereact/toast";
import { ReactElement, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Redirect } from "react-router-dom";
import { getRegisterUser } from "store/user/actions";
import { selectUserId } from "store/user/selectors";

function RegisterPage(): ReactElement {
  const dispatch = useDispatch();
  const userId = useSelector(selectUserId);
  const toast = useRef<Toast>(null);

  const formik = useFormik({
    initialValues: formInitValues,
    onSubmit: (values) => onSubmit(values),
    validate,
  });

  const onSubmit = async (model: FormModel) => {
    dispatch(
      getRegisterUser(model.userName, model.email, model.password, model.passwordConfirmation)
    );
  };

  if (userId) {
    return <Redirect to="/dashboard" />;
  }
  return (
    <>
      <form onSubmit={formik.handleSubmit} autoComplete="off">
        <div>
          <label>Nazwa użytkownika</label>
          <input
            id="userName"
            name="userName"
            value={formik.values.userName}
            onChange={formik.handleChange}
            type="text"
            autoComplete="off"
          />
          {formik.errors.userName && formik.touched.userName ? (
            <div>{formik.errors.userName}</div>
          ) : null}
        </div>

        <div>
          <label>E-mail</label>
          <input
            id="email"
            name="email"
            value={formik.values.email}
            onChange={formik.handleChange}
            type="email"
            autoComplete="off"
          />
          {formik.errors.email && formik.touched.email ? <div>{formik.errors.email}</div> : null}
        </div>

        <div>
          <label>Hasło</label>
          <input
            id="password"
            name="password"
            value={formik.values.password}
            onChange={formik.handleChange}
            type="password"
            autoComplete="off"
          />
          {formik.errors.password && formik.touched.password ? (
            <div>{formik.errors.password}</div>
          ) : null}
        </div>

        <div>
          <label>Powtórz hasło</label>
          <input
            id="passwordConfirmation"
            name="passwordConfirmation"
            value={formik.values.passwordConfirmation}
            onChange={formik.handleChange}
            type="password"
            autoComplete="off"
          />
          {formik.errors.passwordConfirmation && formik.touched.passwordConfirmation ? (
            <div>{formik.errors.passwordConfirmation}</div>
          ) : null}
        </div>
        <input type="submit" value="Utworz konto" />
      </form>
      <Toast ref={toast} />
    </>
  );
}

export default RegisterPage;

interface FormModel {
  userName: string;
  email: string;
  password: string;
  passwordConfirmation: string;
}

const formInitValues: FormModel = {
  userName: "",
  email: "",
  password: "",
  passwordConfirmation: "",
};

const validate = (values: FormModel): FormModel => {
  const errors = {} as FormModel;
  if (!values.userName?.length) {
    errors.userName = "Pole jest wymagane";
  }
  if (!values.email?.length) {
    errors.email = "Pole jest wymagane";
  }
  if (!values.password?.length) {
    errors.password = "Pole jest wymagane";
  }
  if (!values.passwordConfirmation?.length) {
    errors.passwordConfirmation = "Pole jest wymagane";
  }
  if (values.passwordConfirmation !== values.password) {
    errors.passwordConfirmation = "Hasła muszą być takie same";
  }

  return errors;
};
