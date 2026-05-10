import "./LoginPage.css"
import { useState } from "react"
import { useAuth } from "../../auth/AuthContext"
import { StatusState } from "../../components/common/StatusState"
import { getDefaultPathForRole } from "../../layout/navigation"

type LoginPageProps = {
  navigate: (path: string) => void
}

export function LoginPage({ navigate }: LoginPageProps) {
  const { login } = useAuth()
  const [loginValue, setLoginValue] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState<string | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setIsSubmitting(true)
    setError(null)

    try {
      const user = await login({
        login: loginValue.trim(),
        password,
      })

      navigate(getDefaultPathForRole(user.role))
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося виконати вхід.")
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="login-page">
      <section className="login-card">
        <div className="login-card__header">
          <h1>Вхід до системи</h1>
          <p>Увійдіть під обліковим записом адміністратора або студента.</p>
        </div>

        {error ? <StatusState tone="error" message={error} /> : null}

        <form className="login-form" onSubmit={handleSubmit}>
          <label>
            Логін або email
            <input
              type="text"
              value={loginValue}
              autoComplete="username"
              onChange={(event) => setLoginValue(event.target.value)}
            />
          </label>

          <label>
            Пароль
            <input
              type="password"
              value={password}
              autoComplete="current-password"
              onChange={(event) => setPassword(event.target.value)}
            />
          </label>

          <button type="submit" disabled={isSubmitting || loginValue.trim().length === 0 || password.length === 0}>
            {isSubmitting ? "Вхід..." : "Увійти"}
          </button>
        </form>
      </section>
    </div>
  )
}
