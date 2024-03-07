import { act, fireEvent } from '@testing-library/react'

export function inputInsert(
  container: HTMLElement,
  selector: string,
  value: string
): Promise<boolean> {
  const nameEl = container.querySelector(selector) as Element
  return act(() => fireEvent.change(nameEl, { target: { value } }))
}

export function formSubmit(container: HTMLElement, selector: string): Promise<boolean> {
  const form = container.querySelector(selector) as Element
  return act(() => fireEvent.submit(form))
}

export function elementClick(container: HTMLElement, selector: string): Promise<boolean> {
  const element = container.querySelector(selector) as Element
  return act(() => fireEvent.click(element))
}

export function directElementClick(element: HTMLElement): Promise<boolean> {
  return act(() => fireEvent.click(element))
}

export function buttonClick(container: HTMLElement, label: string): Promise<boolean> {
  const buttons = container.querySelectorAll('button')
  const searchingButton = Array.from(buttons).find((button) => button.innerText.includes(label))
  if (!searchingButton) {
    throw Error(`Button with label: ${label} hasn't been found`)
  }
  return act(() => fireEvent.click(searchingButton))
}
