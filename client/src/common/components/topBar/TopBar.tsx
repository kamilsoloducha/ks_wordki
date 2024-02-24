import "./TopBar.scss";
import { FormEvent, ReactElement, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

export default function TopBar({ isLogin }: TopBarProps): ReactElement {
  const navigate = useNavigate();
  const [searchingTerm, setSearchingTerm] = useState("");
  const submitSearch = (e: FormEvent<HTMLFormElement>) => {
    if (searchingTerm.trim().length === 0) {
      return;
    }
    e.preventDefault();
    navigate(`/test?query=${searchingTerm}&dic=Diki`);
  };
  return (
    <div className="top-bar">
      <div className="tob-bar-breadcrumbs">
        <Link className="top-bar-logo" to="/dashboard">
          Wordki
        </Link>
      </div>
      <ul>
        {isLogin && (
          <>
            <li>
              <form onSubmit={submitSearch}>
                <input
                  type="search"
                  value={searchingTerm}
                  onChange={(e) => setSearchingTerm(e.target.value)}
                  placeholder="Search..."
                />
              </form>
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

type TopBarProps = {
  isLogin: boolean;
};
