type SpinnerProps = {
  big?: boolean
  label?: string
}

export function Spinner({ big = false, label = 'Завантаження' }: SpinnerProps) {
  const className = big ? 'spinner spinner-big' : 'spinner'
  return <span className={className} aria-label={label} />
}
