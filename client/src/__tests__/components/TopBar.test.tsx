import TopBar from 'common/components/TopBar'
import { render, screen } from '@testing-library/react'
import * as mock from 'hooks/useUserStorage'
import * as navigateMock from 'react-router-dom'
import { UserSession } from 'hooks/useUserStorage'
import { ReactNode } from 'react'
import { formSubmit, inputInsert } from 'src/__mocks__/utils.test'

vi.mock('hooks/useUserStorage', async (importOriginal) => {
  return {
    ...(await importOriginal<typeof import('hooks/useUserStorage')>()),
    useUserStorage: vi.fn()
  }
})

vi.spyOn(navigateMock, 'useNavigate')
vi.mock('react-router-dom', async (importOriginal) => {
  const actual = await vi.importActual('react-router-dom')
  return {
    ...actual,
    useNavigate: vi.fn()
  }
})

describe('TopBar', () => {
  const navigateFnMock = vi.fn()
  let container: HTMLElement

  beforeEach(() => {
    vi.spyOn(navigateMock, 'useNavigate').mockImplementation(() => {
      return navigateFnMock
    })
  })

  afterEach(() => {
    vi.clearAllMocks()
    vi.resetAllMocks()
  })

  describe('when user is logged in', () => {
    beforeEach(() => {
      vi.spyOn(mock, 'useUserStorage').mockImplementation(() => {
        return {
          remove: () => {},
          get: () => {
            return {} as mock.UserSession
          },
          set: (_: UserSession) => {}
        }
      })

      container = render(testComponent()).container
    })

    it('should render logo', async () => {
      expect(screen.getByText('Wordki', { exact: false })).toBeInTheDocument()
    })

    it('should render logout button', async () => {
      const link = container.querySelector('a[href="/logout"]') as HTMLLinkElement
      expect(link).toBeTruthy()
    })

    it('should render search', async () => {
      const input = container.querySelector('input') as HTMLInputElement
      expect(input).toBeTruthy()

      await inputInsert(container, 'input', 'test-value')
      await formSubmit(container, 'form')

      expect(navigateFnMock).toHaveBeenCalledOnce()
      expect(navigateFnMock).toHaveBeenCalledWith('/test?query=test-value&dic=Diki')
    })
  })

  describe('when user is not logged in', () => {
    beforeEach(() => {
      vi.spyOn(mock, 'useUserStorage').mockImplementation(() => {
        return {
          remove: () => {},
          get: () => {
            return undefined
          },
          set: (_: UserSession) => {}
        }
      })

      container = render(testComponent()).container
    })

    it('should render logo', async () => {
      expect(screen.getByText('Wordki', { exact: false })).toBeInTheDocument()
    })

    it('should render login button', async () => {
      const link = container.querySelector('a[href="/login"]') as HTMLLinkElement
      expect(link).toBeTruthy()
    })

    it('should render register button', async () => {
      const link = container.querySelector('a[href="/register"]') as HTMLLinkElement
      expect(link).toBeTruthy()
    })
  })
})

const testComponent: () => ReactNode = () => {
  return (
    <navigateMock.MemoryRouter>
      <TopBar />
    </navigateMock.MemoryRouter>
  )
}
