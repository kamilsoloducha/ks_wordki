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
    <div className="flex w-full items-center justify-between">
      <Link className="logo ml-5" to="/dashboard">
        Wordki
      </Link>
      <ul className="m-0 flex list-none items-center p-0">
        {isLogin && (
          <>
            <li>
              <form onSubmit={submitSearch}>
                <input
                  className="text-l w-[125px] rounded-md border-2 border-zinc-600 bg-neutral-800 p-3 text-zinc-400 transition-width focus:w-[300px] focus:bg-neutral-700"
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
