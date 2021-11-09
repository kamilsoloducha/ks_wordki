import { ReactElement, ReactNode } from "react";
import { useSelector } from "react-redux";
import { selectToken } from "store/user/selectors";
import http from "../../services/http/http";

interface Model {
  children: ReactNode;
}

function AxiosEx({ children }: Model) {
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
  return children as ReactElement<any>;
}

export default AxiosEx;
