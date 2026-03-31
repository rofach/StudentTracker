import { Spinner } from './Spinner'

type LoadingOverlayProps = {
  className: string
  message: string
}

export function LoadingOverlay({ className, message }: LoadingOverlayProps) {
  return (
    <div className={className}>
      <Spinner big />
      <p>{message}</p>
    </div>
  )
}
