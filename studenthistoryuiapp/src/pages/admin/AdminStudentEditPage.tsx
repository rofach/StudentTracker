import { useEffect, useState } from "react"
import { changeStudentStatus, getStudentById, updateStudent } from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { StudentUpdateDto } from "../../types/api"

type AdminStudentEditPageProps = {
  studentId: number
  navigate: (path: string) => void
}

const emptyForm: StudentUpdateDto = {
  firstName: "",
  lastName: "",
  birthDate: null,
  email: null,
  phone: null,
}

export function AdminStudentEditPage({ studentId, navigate }: AdminStudentEditPageProps) {
  const [form, setForm] = useState<StudentUpdateDto>(emptyForm)
  const [statusValue, setStatusValue] = useState("Active")
  const [title, setTitle] = useState("Редагування студента")
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

        setTitle(`${result.lastName} ${result.firstName}`)
        setForm({
          firstName: result.firstName,
          lastName: result.lastName,
          birthDate: result.birthDate,
          email: result.email,
          phone: result.phone,
        })
        setStatusValue(result.status)
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
      await updateStudent(studentId, form)
      setMessage("Базові дані студента оновлено.")
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося оновити дані студента.")
    } finally {
      setIsSaving(false)
    }
  }

  const handleChangeStatus = async () => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      await changeStudentStatus(studentId, { status: statusValue })
      setMessage("Статус студента оновлено.")
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося змінити статус студента.")
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

  return (
    <div className="page-stack">
      <PageHeader
        title={title}
        description="Редагування базових даних студента."
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
        <h2>Статус</h2>
        <div className="filters-row">
          <label>
            Поточний статус
            <select value={statusValue} onChange={(event) => setStatusValue(event.target.value)}>
              <option value="Active">Активний</option>
              <option value="OnLeave">Академвідпустка</option>
              <option value="Expelled">Відрахований</option>
              <option value="Graduated">Випускник</option>
            </select>
          </label>

          <button type="button" onClick={handleChangeStatus} disabled={isSaving}>
            {isSaving ? "Оновлення..." : "Змінити статус"}
          </button>
        </div>
      </section>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}
