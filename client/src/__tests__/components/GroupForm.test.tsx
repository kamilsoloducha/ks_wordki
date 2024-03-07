import { GroupForm, GroupFormModel } from 'common/components/GroupForm'
import { render, screen } from '@testing-library/react'
import { formSubmit, inputInsert } from 'src/__mocks__/utils.test'

describe('GroupForm', () => {
  const onSubmitFn = vi.fn()

  afterEach(() => {
    vi.clearAllMocks()
  })

  it('should render all elements', async () => {
    const { container } = render(<GroupForm onSubmit={onSubmitFn} />)

    expect(container.querySelector('#name')).toBeInTheDocument()
    expect(container.querySelector('#front')).toBeInTheDocument()
    expect(container.querySelector('#back')).toBeInTheDocument()
  })

  it('should set initial values', async () => {
    const initailFormModel: GroupFormModel = {
      name: 'InitialName',
      front: 'InitialFront',
      back: 'InitialBack'
    }

    const options: string[] = ['InitialFront', 'InitialBack']
    const { container } = render(
      <GroupForm onSubmit={onSubmitFn} group={initailFormModel} options={options} />
    )

    const input = container.querySelector('#name') as HTMLInputElement
    expect(input.value).toBe('InitialName')

    expect(screen.getByText('InitialFront')).toBeInTheDocument()
    expect(screen.getByText('InitialBack')).toBeInTheDocument()
  })

  it('should call onSubmitFn after editing', async () => {
    const initailFormModel: GroupFormModel = {
      name: 'InitialName',
      front: 'InitialFront',
      back: 'InitialBack'
    }

    const options: string[] = ['InitialFront', 'InitialBack']
    const { container } = render(
      <GroupForm onSubmit={onSubmitFn} group={initailFormModel} options={options} />
    )

    await inputInsert(container, '#name', 'NewInitialName')

    await formSubmit(container, 'form')

    expect(onSubmitFn).toHaveBeenCalledOnce()
    expect(onSubmitFn).toHaveBeenCalledWith({
      name: 'NewInitialName',
      front: 'InitialFront',
      back: 'InitialBack'
    } as GroupFormModel)
  })
})
