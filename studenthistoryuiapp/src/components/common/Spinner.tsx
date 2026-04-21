import "./Spinner.css"

type SpinnerProps = {
  label?: string
}

export function Spinner({ label = "Завантаження..." }: SpinnerProps) {
  return (
    <div className="spinner-wrap" role="status" aria-live="polite">
      <div className="spinner" />
      <span>{label}</span>
    </div>
  )
}
