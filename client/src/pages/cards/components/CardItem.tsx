import { ReactElement } from 'react'
import { CardSummary, SideSummary } from 'pages/cards/models'
import { drawerMap } from 'src/assets/consts'

export type CardItemProps = {
  card: CardSummary
}

export function CardItem({ card }: CardItemProps): ReactElement {
  return (
    <div className="w-full flex justify-between border-b-accent-dark hover:bg-lighter-a-bit">
      <div className="w-3/4 ml-2 my-5">
        <Side side={card.front} />
        <Side side={card.back} />
      </div>
    </div>
  )
}

function Side({ side }: { side: SideSummary }): ReactElement {
  const extendedText = convertIntoExtendedText(side.value)

  return (
    <div className={`p-2 border-l`} style={{ borderColor: drawerMap.get(side.drawer) }}>
      <div className="mb-1 text-3xl text-accent-super-light">
        <strong>
          {extendedText.map((x, index) => (
            <span key={index} className={x.isLowlight ? 'text-accent-light' : ''}>
              {x.text}
            </span>
          ))}
        </strong>
      </div>
      <div>{side.example}</div>
    </div>
  )
}

export function convertIntoExtendedText(text: string): { text: string; isLowlight: boolean }[] {
  const result: { text: string; isLowlight: boolean }[] = []
  if (!text.includes('{')) {
    result.push({ text, isLowlight: false })
    return result
  }
  const begins = []
  const ends = []

  for (let i = 0; i < text.length; i++) {
    const element = text[i]
    if (element === '{') {
      begins.push(i)
      continue
    }
    if (element === '}') {
      ends.push(i)
      continue
    }
  }

  if (!text.startsWith('{')) {
    result.push({ text: text.slice(0, begins[0]), isLowlight: false })
  }

  for (let i = 0; i < begins.length; i++) {
    result.push({ text: text.slice(begins[i] + 1, ends[i]), isLowlight: true })
    if (i + 1 < begins.length) {
      const begin = ends[i] + 1
      const end = begins[i + 1]
      if (begin !== end)
        result.push({ text: text.slice(ends[i] + 1, begins[i + 1]), isLowlight: false })
    }
  }

  if (!text.endsWith('}')) {
    result.push({ text: text.slice(ends[ends.length - 1] + 1), isLowlight: false })
  }

  return result
}
