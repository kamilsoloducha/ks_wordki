import { ReactElement, ReactNode } from "react";
import { useSelector } from "react-redux";
import { selectToken } from "store/user/selectors";
import http from "../../../api/services/httpBase";
import history from "../../services/history";

export default function AxiosEx({ children }: Model) {
  const token = useSelector(selectToken);

  http.interceptors.request.use(
    (req) => {
      if (token) {
        req.headers = {
          Authorization: "Bearer " + token,
          ...req.headers,
        };
      }
      return req;
    },
    (error) => {
      console.error(error);
    }
  );

  http.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error?.response?.status === 401) {
        history?.push("/logout");
      }
      return error;
    }
  );
  return children as ReactElement<any>;
}

interface Model {
  children: ReactNode;
}
