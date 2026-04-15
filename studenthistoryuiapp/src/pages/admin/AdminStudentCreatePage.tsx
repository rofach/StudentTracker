import { useState } from "react"
import { createStudent } from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { StatusState } from "../../components/common/StatusState"
import type { StudentCreateDto } from "../../types/api"

type AdminStudentCreatePageProps = {
  navigate: (path: string) => void
}

const emptyForm: StudentCreateDto = {
  firstName: "",
  lastName: "",
  patronymic: null,
  birthDate: null,
  email: null,
  phone: null,
}

export function AdminStudentCreatePage({ navigate }: AdminStudentCreatePageProps) {
  const [form, setForm] = useState<StudentCreateDto>(emptyForm)
  const [message, setMessage] = useState<string | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  const handleCreate = async () => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      const created = await createStudent(form)
      setMessage("Студента успішно створено.")
      navigate(`/admin/students/${created.studentId}`)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося створити студента.")
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Новий студент"
        description="Створення базової картки студента."
        actions={
          <button type="button" onClick={() => navigate("/admin/students")}>
            Назад до списку
          </button>
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

        <div className="inline-actions">
          <button type="button" onClick={handleCreate} disabled={isSaving}>
            {isSaving ? "Збереження..." : "Створити студента"}
          </button>
          <button type="button" onClick={() => navigate("/admin/students")}>
            Скасувати
          </button>
        </div>
      </section>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}
