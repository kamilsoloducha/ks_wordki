import TopBar from 'common/components/TopBar'
import { ReactNode } from 'react'
import { Outlet } from 'react-router-dom'

export function Root(): ReactNode {
  return (
    <>
      <TopBar />
      <div className="px-10 pt-5">
        <Outlet />
      </div>
    </>
  )
}
