import { createContext, useCallback, useContext, useMemo, useState, type ReactNode } from "react"
import "./ToastCenter.css"

type ToastTone = "info" | "error"

type ToastInput = {
  tone: ToastTone
  message: string
  title?: string
  durationMs?: number
}

type ToastRecord = ToastInput & {
  id: string
}

type ToastContextValue = {
  pushToast: (toast: ToastInput) => void
}

const ToastContext = createContext<ToastContextValue | null>(null)

function fallbackTitle(tone: ToastTone) {
  return tone === "error" ? "Помилка" : "Повідомлення"
}

export function ToastProvider({ children }: { children: ReactNode }) {
  const [toasts, setToasts] = useState<ToastRecord[]>([])

  const removeToast = useCallback((id: string) => {
    setToasts((current) => current.filter((toast) => toast.id !== id))
  }, [])

  const pushToast = useCallback(
    ({ tone, message, title, durationMs = 10000 }: ToastInput) => {
      const id = `${Date.now()}-${Math.random().toString(36).slice(2, 8)}`
      setToasts((current) => [...current, { id, tone, message, title, durationMs }])

      window.setTimeout(() => {
        removeToast(id)
      }, durationMs)
    },
    [removeToast],
  )

  const value = useMemo(() => ({ pushToast }), [pushToast])

  return (
    <ToastContext.Provider value={value}>
      {children}
      <div className="toast-center" aria-live="polite" aria-atomic="true">
        {toasts.map((toast) => (
          <div key={toast.id} className={`toast toast--${toast.tone}`} role={toast.tone === "error" ? "alert" : "status"}>
            <div className="toast__title">{toast.title ?? fallbackTitle(toast.tone)}</div>
            <div className="toast__message">{toast.message}</div>
          </div>
        ))}
      </div>
    </ToastContext.Provider>
  )
}

export function useToast() {
  const context = useContext(ToastContext)

  if (!context) {
    throw new Error("useToast must be used inside ToastProvider.")
  }

  return context
}
