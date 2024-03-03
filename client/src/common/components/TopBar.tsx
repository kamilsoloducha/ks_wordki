import { useUserStorage } from 'common/hooks'
import { FormEvent, ReactElement, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'

export default function TopBar(): ReactElement {
  const { get } = useUserStorage()
  const navigate = useNavigate()
  const [searchingTerm, setSearchingTerm] = useState('')
  const isLogin = get()
  const submitSearch = (e: FormEvent<HTMLFormElement>) => {
    if (searchingTerm.trim().length === 0) {
      return
    }
    e.preventDefault()
    setSearchingTerm('')
    navigate(`/test?query=${searchingTerm}&dic=Diki`)
  }
  return (
    <div className="w-full flex justify-between items-center">
      <Link className="logo ml-5" to="/dashboard">
        Wordki
      </Link>
      <ul className="flex items-center list-none m-0 p-0">
        {isLogin && (
          <>
            <li>
              <form onSubmit={submitSearch}>
                <input
                  className="p-3 text-l rounded-md w-[125px] focus:w-[300px] transition-width bg-neutral-800 focus:bg-neutral-700 text-zinc-400 border-2 border-zinc-600"
                  type="search"
                  value={searchingTerm}
                  onChange={(e) => setSearchingTerm(e.target.value)}
                  placeholder="Search..."
                />
              </form>
            </li>
            <li>
              <Link className="top-bar-button" to="/logout">
                Logout
              </Link>
            </li>
          </>
        )}
        {!isLogin && (
          <>
            <li>
              <Link className="top-bar-button" to="/login">
                Login
              </Link>
            </li>
            <li>
              <Link className="top-bar-button" to="/register">
                Register
              </Link>
            </li>
          </>
        )}
      </ul>
    </div>
  )
}

type TopBarProps = {
  isLogin: boolean
}
