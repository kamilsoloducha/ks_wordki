import UserRepeat from 'pages/lesson/models/userRepeat'
import { ReactElement } from 'react'
import './History.scss'

export function History({ history, userAnswer = true, onItemClick }: Model): ReactElement {
  const isClickable = onItemClick !== undefined && onItemClick !== null
  const itemClick = (item: UserRepeat) => {
    if (onItemClick) {
      onItemClick(item)
    }
  }

  return (
    <table>
      <thead>
        <tr>
          <th className="icon"></th>
          <th>Question</th>
          <th>Answer</th>
          {userAnswer && <th>User answer</th>}
        </tr>
      </thead>
      <tbody>
        {history.map((item, index) => (
          <tr
            key={index}
            onClick={() => itemClick(item)}
            className={`${isClickable ? 'clickable' : ''}`}
          >
            <td className={getClassByResult(item.result)}>
              <img
                className="history-result-icon"
                alt={getIconByResult(item.result)}
                src={`/svgs/${getIconByResult(item.result)}`}
              />
            </td>
            <td>{item.repeat.question}</td>
            <td>{item.repeat.answer}</td>
            {userAnswer && <td>{item.userAnswer}</td>}
          </tr>
        ))}
      </tbody>
    </table>
  )
}

interface Model {
  history: UserRepeat[]
  userAnswer?: boolean
  onItemClick?: (item: UserRepeat) => void
}

function getClassByResult(result: number): string {
  if (result < 0) {
    return 'history-wrong'
  } else if (result > 0) {
    return 'history-correct'
  } else {
    return 'history-accepted'
  }
}

function getIconByResult(result: number): string {
  if (result < 0) {
    return 'wrong.svg'
  } else if (result > 0) {
    return 'correct.svg'
  } else {
    return 'accepted.svg'
  }
}
