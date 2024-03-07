import { ReactElement } from 'react'

export default function Question({ value, language, example }: Model): ReactElement {
  return (
    <div className="flex h-[30vh] w-full select-none flex-col flex-wrap justify-center gap-5 text-center md:select-auto">
      <div className="w-full text-4xl">{value}</div>
      <div className="w-full text-xl">{example}</div>
    </div>
  )
}

interface Model {
  value: string
  language: number
  example?: string
}
