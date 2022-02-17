import { ReactElement, useEffect } from "react";
import { useDispatch } from "react-redux";
import { Redirect } from "react-router";
import { getLogoutUser } from "store/user/actions";

function LogoutPage(): ReactElement {
  const dispatch = useDispatch();
  useEffect(() => {
    localStorage.removeItem("id");
    localStorage.removeItem("token");
    dispatch(getLogoutUser());
  }, [dispatch]);
  return <Redirect to="" />;
}

export default LogoutPage;
