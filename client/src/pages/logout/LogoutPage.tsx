import { ReactElement, useEffect } from "react";
import { Redirect } from "react-router";
import { useAppDispatch } from "store/store";
import { logout } from "store/user/reducer";

function LogoutPage(): ReactElement {
  const dispatch = useAppDispatch();
  useEffect(() => {
    localStorage.removeItem("id");
    localStorage.removeItem("token");
    dispatch(logout());
  }, [dispatch]);
  return <Redirect to="" />;
}

export default LogoutPage;
