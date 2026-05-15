import { useEffect, useState } from "react"
import {
  deleteStudent,
  getStudentById,
  resetStudentPassword,
  updateStudent,
} from "../../api/studentsApi"
import { useToast } from "../../components/common/ToastCenter"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type {
  EntityId,
  StudentAccountPasswordDto,
  StudentUpdateDto,
} from "../../types/api"
import { fullName } from "../../utils/format"

type AdminStudentEditPageProps = {
  studentId: EntityId
  navigate: (path: string) => void
}

const emptyForm: StudentUpdateDto = {
  firstName: "",
  lastName: "",
  patronymic: null,
  birthDate: null,
  email: null,
  phone: null,
}

export function AdminStudentEditPage({ studentId, navigate }: AdminStudentEditPageProps) {
  const { pushToast } = useToast()
  const [form, setForm] = useState<StudentUpdateDto>(emptyForm)
  const [savedEmail, setSavedEmail] = useState<string | null>(null)
  const [title, setTitle] = useState("Редагування студента")
  const [issuedPassword, setIssuedPassword] = useState<StudentAccountPasswordDto | null>(null)
  const [customPassword, setCustomPassword] = useState("")
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    getStudentById(studentId)
      .then((result) => {
        if (!isActive) {
          return
        }

        setTitle(fullName(result.firstName, result.lastName, result.patronymic))
        setForm({
          firstName: result.firstName,
          lastName: result.lastName,
          patronymic: result.patronymic,
          birthDate: result.birthDate,
          email: result.email,
          phone: result.phone,
        })
        setSavedEmail(result.email)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити дані студента.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [studentId])

  const handleUpdateProfile = async () => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      const updated = await updateStudent(studentId, form)
      setSavedEmail(updated.email)
      setMessage("Базові дані студента оновлено.")
      pushToast({ tone: "info", title: "Успішно", message: "Дані студента оновлено." })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося оновити дані студента."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }


  const handleResetPassword = async (newPassword: string | null) => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      const result = await resetStudentPassword(studentId, { newPassword })
      setIssuedPassword(result)
      setCustomPassword("")
      setMessage(newPassword ? "Пароль студентського акаунта оновлено." : "Новий пароль згенеровано.")
      pushToast({
        tone: "info",
        title: "Успішно",
        message: newPassword ? "Пароль студентського акаунта оновлено." : "Згенеровано новий пароль.",
      })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося оновити пароль."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }

  const handleDeleteStudent = async () => {
    const confirmed = window.confirm(
      "Видалити студента разом із пов'язаними зарахуваннями, підгрупами, оцінками, академвідпустками, зовнішніми переведеннями та обліковим записом?",
    )

    if (!confirmed) {
      return
    }

    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      await deleteStudent(studentId)
      pushToast({ tone: "info", title: "Успішно", message: "Студента видалено." })
      navigate("/admin/students")
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося видалити студента."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }

  if (isLoading) {
    return <Spinner label="Завантаження форми редагування..." />
  }

  if (error && !message) {
    return <StatusState tone="error" message={error} />
  }

  const emailChanged = (form.email ?? "").trim() !== (savedEmail ?? "").trim()

  return (
    <div className="page-stack">
      <PageHeader
        title={title}
        description="Редагування студента."
        actions={
          <div className="inline-actions">
            <button type="button" onClick={() => navigate(`/admin/students/${studentId}`)}>
              До картки студента
            </button>
            <button type="button" onClick={() => navigate("/admin/students")}>
              Назад до списку
            </button>
          </div>
        }
      />

      <section className="panel">
        <h2>Базові дані</h2>
        <div className="form-grid">
          <label>
            Ім'я
            <input
              type="text"
              value={form.firstName}
              onChange={(event) => setForm((prev) => ({ ...prev, firstName: event.target.value }))}
            />
          </label>
          <label>
            Прізвище
            <input
              type="text"
              value={form.lastName}
              onChange={(event) => setForm((prev) => ({ ...prev, lastName: event.target.value }))}
            />
          </label>
          <label>
            По батькові
            <input
              type="text"
              value={form.patronymic ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, patronymic: event.target.value || null }))}
            />
          </label>
          <label>
            Дата народження
            <input
              type="date"
              value={form.birthDate ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, birthDate: event.target.value || null }))}
            />
          </label>
          <label>
            Email
            <input
              type="email"
              value={form.email ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, email: event.target.value || null }))}
            />
          </label>
          <label>
            Телефон
            <input
              type="text"
              value={form.phone ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, phone: event.target.value || null }))}
            />
          </label>
        </div>

        <button type="button" onClick={handleUpdateProfile} disabled={isSaving}>
          {isSaving ? "Збереження..." : "Оновити дані"}
        </button>
      </section>


      <section className="panel">
        <h2>Обліковий запис</h2>
        <div className="summary-grid">
          <div>
            <strong>Логін:</strong> {savedEmail ?? "-"}
          </div>
          <div>
            <strong>Створення пароля:</strong> вручну або автоматично
          </div>
        </div>

        {emailChanged ? (
          <StatusState
            tone="info"
            message="Щоб змінити логін для входу, спочатку збережіть новий email у базових даних."
          />
        ) : null}

        <div className="form-grid">
          <label>
            Новий пароль вручну
            <input
              type="text"
              value={customPassword}
              onChange={(event) => setCustomPassword(event.target.value)}
              placeholder="Введіть новий пароль"
            />
          </label>
        </div>

        <div className="inline-actions">
          <button
            type="button"
            onClick={() => handleResetPassword(customPassword.trim() || null)}
            disabled={isSaving || emailChanged}
          >
            {customPassword.trim().length > 0 ? "Встановити цей пароль" : "Згенерувати новий пароль"}
          </button>
        </div>

        {issuedPassword ? (
          <div className="form-grid">
            <label>
              Логін
              <input type="text" readOnly value={issuedPassword.login} />
            </label>
            <label>
              Пароль
              <input type="text" readOnly value={issuedPassword.password} />
            </label>
          </div>
        ) : null}
      </section>

      <section className="panel">
        <h2>Видалення студента</h2>
        <button type="button" className="button-danger" onClick={handleDeleteStudent} disabled={isSaving}>
          {isSaving ? "Виконання..." : "Видалити студента"}
        </button>
      </section>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}
