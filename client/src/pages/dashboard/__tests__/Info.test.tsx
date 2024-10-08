import { Info } from 'pages/dashboard/components/Info'
import { render, fireEvent, screen } from '@testing-library/react'

describe('Info', () => {
  it('renders title and value correctly', () => {
    render(<Info title="Test Title" value="Test Value" />)
    expect(screen.getByText('Test Title')).toBeInTheDocument()
    expect(screen.getByText('Test Value')).toBeInTheDocument()
  })

  it('executes onClick function when clicked', () => {
    const onClickMock = vi.fn()
    render(<Info title="Test Title" value="Test Value" onClick={onClickMock} />)
    fireEvent.click(screen.getByText('Test Title')) // Simulate click
    expect(onClickMock).toHaveBeenCalledTimes(1) // Make sure onClick function is called
  })
})
