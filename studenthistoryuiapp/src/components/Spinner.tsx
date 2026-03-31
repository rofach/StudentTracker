type SpinnerProps = {
  big?: boolean
  label?: string
}

export function Spinner({ big = false, label = 'Завантаження' }: SpinnerProps) {
  const spinnerClassName = big ? 'spinner spinner-big' : 'spinner'

  return <span className={spinnerClassName} aria-label={label} />
}
