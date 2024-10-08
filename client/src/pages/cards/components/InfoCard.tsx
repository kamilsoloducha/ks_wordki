import { ReactElement } from 'react'

type InfoCardProps = {
  value: string | number
  label: string | number
  classNameOverriden?: string
  disabled?: boolean
  onClick?: () => void
}

export default function InfoCard({
  value = 'test',
  label = 'test',
  classNameOverriden,
  disabled: disable = false,
  onClick
}: InfoCardProps): ReactElement {
  return (
    <div
      className={`h-[120px] w-[250px] relative rounded overflow-hidden flex justify-end flex-col ${classNameOverriden} ${
        disable ? 'cursor-none' : ''
      }
      ${onClick ? 'hover:bg-lighter-a-bit' : ''}`}
      onClick={onClick}
    >
      <div className="w-full ml-3 text-white">{label}</div>
      <div className="w-full ml-3 font-bold text-white">{value}</div>
      <svg className="absolute bottom-0 left-0 w-full" viewBox="0 0 10 5">
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
