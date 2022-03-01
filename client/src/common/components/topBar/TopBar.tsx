import "./TopBar.scss";
import React, { ReactElement } from "react";
import { Link } from "react-router-dom";
import { Breadcrumb } from "store/root/state";

export default function TopBar({ isLogin, breadCrumbs }: Model): ReactElement {
  return (
    <div className="top-bar">
      <div className="tob-bar-breadcrumbs">
        <Link className="top-bar-logo" to="/dashboard">
          Wordki
        </Link>
        {breadCrumbs?.map((item, index) => (
          <React.Fragment key={index}>
            <li className="tob-bar-separator"></li> {<Link to={item.url ?? ""}>{item.name}</Link>}
          </React.Fragment>
        ))}
      </div>
      <ul>
        {isLogin && (
          <>
            <li></li>
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
  breadCrumbs?: Breadcrumb[];
}
