import { ReactElement, useEffect } from "react";
import { useDispatch } from "react-redux";
import { Redirect } from "react-router";
import { logout } from "store/user/actions";

function LogoutPage(): ReactElement {
  const dispatch = useDispatch();
  useEffect(() => {
    localStorage.removeItem("id");
    localStorage.removeItem("token");
    dispatch(logout());
  }, [dispatch]);
  return <Redirect to="" />;
}

export default LogoutPage;
