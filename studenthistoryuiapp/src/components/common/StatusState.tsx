type StatusStateProps = {
  tone: "info" | "error"
  message: string
}

export function StatusState({ tone, message }: StatusStateProps) {
  return <div className={`status-state status-state--${tone}`}>{message}</div>
}
