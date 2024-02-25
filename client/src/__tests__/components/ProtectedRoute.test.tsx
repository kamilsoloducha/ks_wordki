import ProtectedRoute from 'common/components/ProtectedRoute'
import { render, screen } from '@testing-library/react'
import * as mock from 'hooks/useUserStorage'
import { UserSession } from 'hooks/useUserStorage'
import { NavigateProps, To } from 'react-router-dom'

vi.mock('hooks/useUserStorage', async (importOriginal) => {
  return {
    ...(await importOriginal<typeof import('hooks/useUserStorage')>()),
    useUserStorage: vi.fn()
  }
})

vi.mock('react-router-dom', async (importOriginal) => {
  return {
    ...(await importOriginal<typeof import('react-router-dom')>()),
    Navigate: (props: NavigateProps) => <>Navigate {props.to}</>
  }
})

describe('ProtectedRoute', () => {
  ;[
    {
      isLoginForbiden: false,
      isLoginRequired: true,
      userSession: undefined,
      expectedRender: 'Navigate /login'
    },
    {
      isLoginForbiden: false,
      isLoginRequired: false,
      userSession: undefined,
      expectedRender: 'Children'
    },
    {
      isLoginForbiden: true,
      isLoginRequired: false,
      userSession: undefined,
      expectedRender: 'Children'
    },
    {
      isLoginForbiden: false,
      isLoginRequired: true,
      userSession: { id: 'test' } as UserSession,
      expectedRender: 'Children'
    },
    {
      isLoginForbiden: false,
      isLoginRequired: false,
      userSession: { id: 'test' } as UserSession,
      expectedRender: 'Children'
    },
    {
      isLoginForbiden: true,
      isLoginRequired: false,
      userSession: { id: 'test' } as UserSession,
      expectedRender: 'Navigate /'
    }
  ].forEach((item, index) => {
    it(`should render properly :: ${index}`, () => {
      vi.spyOn(mock, 'useUserStorage').mockImplementation(() => {
        return {
          remove: () => {},
          get: () => item.userSession,
          set: (_: UserSession) => {}
        }
      })
      const { container } = render(
        <ProtectedRoute
          isLoginForbiden={item.isLoginForbiden}
          isLoginRequired={item.isLoginRequired}
        >
          <>Children</>
        </ProtectedRoute>
      )

      expect(container.innerHTML).toBe(item.expectedRender)
    })
  })
})
