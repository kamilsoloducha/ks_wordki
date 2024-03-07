export function Info({ title, value, onClick }: InfoProps) {
  return (
    <div
      className="flex cursor-pointer flex-col rounded-2xl bg-white p-4 hover:bg-slate-200"
      onClick={onClick}
    >
      <div className="text-center text-5xl">{title}</div>
      <div className="text-center text-3xl">{value}</div>
    </div>
  )
}

interface InfoProps {
  title: string
  value: string | number
  onClick?: () => void
}
