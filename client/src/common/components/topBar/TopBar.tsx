import "./TopBar.scss";
import { ReactElement } from "react";
import { Link } from "react-router-dom";

export default function TopBar({ isLogin }: Model): ReactElement {
  return (
    <div className="top-bar">
      <div className="top-bar-logo">Wordki</div>
      <ul>
        {isLogin && (
          <>
            <li>
              <Link to="/dashboard">Dashboard</Link>
            </li>
            <li>
              <Link to="/logout">Logout</Link>
            </li>
          </>
        )}
        {!isLogin && (
          <>
            <li>
              <Link to="/login">Login</Link>
            </li>
            <li>
              <Link to="/register">Register</Link>
            </li>
          </>
        )}
      </ul>
    </div>
  );
}

interface Model {
  isLogin: boolean;
}
