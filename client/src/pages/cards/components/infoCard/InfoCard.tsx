import './InfoCard.scss'
import { ReactElement } from 'react'

export default function InfoCard({
  value = 'test',
  label = 'test',
  classNameOverriden,
  disabled: disable = false,
  onClick
}: InfoCardProps): ReactElement {
  return (
    <div
      className={`info-card-container ${classNameOverriden} ${disable ? 'info-card-disabled' : ''}
      ${onClick ? 'clickable' : ''}`}
      onClick={onClick}
    >
      <div className="info-card-label">{label}</div>
      <div className="info-card-value">{value}</div>
      <svg viewBox="0 0 10 5">
        <defs>
          <clipPath id="clip-0">
            <rect x="0" y="0" width="10" height="5" />
          </clipPath>
        </defs>
        <circle fill="#ffffff33" cx="3" cy="7" r="6" clipPath="#clip-0" />
      </svg>
    </div>
  )
}

type InfoCardProps = {
  value: string | number
  label: string | number
  classNameOverriden?: string
  disabled?: boolean
  onClick?: () => void
}
