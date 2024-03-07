import TopBar from 'common/components/TopBar'
import { ReactNode } from 'react'
import { Outlet } from 'react-router-dom'

export function Root(): ReactNode {
  return (
    <>
      <TopBar />
      <div className="md:px-10 md:pt-5">
        <Outlet />
      </div>
    </>
  )
}
