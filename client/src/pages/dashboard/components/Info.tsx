export function Info({ title, value, onClick }: InfoProps) {
  return (
    <div
      className="flex bg-white rounded-2xl flex-col cursor-pointer hover:bg-slate-200 p-4"
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
